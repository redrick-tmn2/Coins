using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace CoinsApplication.Models
{
    public class ProfileModel : ObservableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public ObservableCollection<CoinModel> Coins { get; }

        public ProfileModel(string name, IEnumerable<CoinModel> coins)
        {
            Name = name;

            Coins = new ObservableCollection<CoinModel>(coins);
        }
    }
}