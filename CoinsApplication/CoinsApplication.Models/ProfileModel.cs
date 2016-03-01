using System.Collections;
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

        private readonly ObservableCollection<CoinModel> _coins = new ObservableCollection<CoinModel>();
        public ObservableCollection<CoinModel> Coins
        {
            get { return _coins; }
        }

        public ProfileModel(string name, IEnumerable<CoinModel> coins)
        {
            Name = name;

            _coins = new ObservableCollection<CoinModel>(coins);
        }
    }
}