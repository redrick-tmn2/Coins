using System.Windows.Media.Imaging;
using CoinsApplication.DAL.Entities;
using CoinsApplication.DAL.Infrastructure;
using CoinsApplication.Misc;
using CoinsApplication.Services.Interfaces;

namespace CoinsApplication.Models
{
    public class CoinModel : DirtyObservableObject
    {

        public int Id { get; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetAndDirty(ref _title, value); }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { SetAndDirty(ref _image, value); }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set { SetAndDirty(ref _year, value); }
        }

        private Country _country;
        public Country Country
        {
            get { return _country; }
            set { SetAndDirty(ref _country, value); }
        }

        private Currency _currency;
        public Currency Currency
        {
            get { return _currency; }
            set { SetAndDirty(ref _currency, value); }
        }

        public CoinModel(IDirtySerializableCacheService serializableCacheService)
            : base(serializableCacheService)
        {
            Currency = new Currency();
            Country = new Country();
        }

        public CoinModel(Coin coin, IDirtySerializableCacheService serializableCacheService)
            : base(serializableCacheService)
        {
            Id = coin.Id;
            _title = coin.Title;
            _image = coin.Image.ToImageSource();
            _year = coin.Year;
            _currency = coin.Currency;
            _country = coin.Country;
        }

        #region IDirtySerializable members
        

        public override IEntity GetEntity()
        {
            return new Coin
            {
                Id = Id,
                Currency = Currency,
                Country = Country,
                Image = Image.ToByteArray(),
                Title = Title,
                Year = Year
            };
        }

        #endregion

    }
}
