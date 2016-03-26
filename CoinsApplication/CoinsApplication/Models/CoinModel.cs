using System.Collections.ObjectModel;
using System.Linq;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.Properties;
using CoinsApplication.Services.Interfaces;
using MvvmValidation;

namespace CoinsApplication.Models
{
    public class CoinModel : ValidatatbleObservableObject
    {
        private Coin _coin;

        public int Id
        {
            get { return _coin.Id; }
        }

        public ObservableCollection<Image> Images { get; } = new ObservableCollection<Image>();
        
        public string Title
        {
            get { return _coin.Title; }
            set
            {
                SetAndDirty(_coin.Title, value, () => _coin.Title = value);
                Validator.Validate(() => Title);
            }
        }

        private Image _selectedImage;
        public Image SelectedImage
        {
            get { return _selectedImage; }
            set { Set(ref _selectedImage, value); }
        }
        
        public int Year
        {
            get { return _coin.Year; }
            set { SetAndDirty(_coin.Year, value, () => _coin.Year = value); }
        }
        
        public Country Country
        {
            get { return _coin.Country; }
            set
            {
                SetAndDirty(_coin.Country, value, () => _coin.Country = value);
                Validator.Validate(() => Country);
            }
        }
        
        public Currency Currency
        {
            get { return _coin.Currency; }
            set
            {
                SetAndDirty(_coin.Currency, value, () => _coin.Currency = value);
                Validator.Validate(() => Currency);
            }
        }

        public CoinModel(IDirtySerializableCacheService serializableCacheService)
            : base(serializableCacheService)
        {
            _coin = new Coin();

            Validator.AddRule(() => Title, () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Title), Resources.ErrorBlankField));
            Validator.AddRule(() => Country, () => RuleResult.Assert(Country != null, Resources.ErrorBlankField));
            Validator.AddRule(() => Currency, () => RuleResult.Assert(Currency != null, Resources.ErrorBlankField));
        }


        public CoinModel(Coin coin, IDirtySerializableCacheService serializableCacheService)
            : this(serializableCacheService)
        {
            _coin = coin;

            Images = new ObservableCollection<Image>(coin.Images);
            SelectedImage = coin.Images.FirstOrDefault();

            Validator.ValidateAll();
        }

        public void AddImage(Image image)
        {
            image.Coin = _coin;
            Images.Add(image);
        }

        #region IDirtySerializable members
        
        public override IEntity GetEntity()
        {
            _coin.Images = Images.ToList();
            return _coin;
        }

        #endregion

    }
}
