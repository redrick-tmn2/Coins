using CoinsApplication.Models;

namespace CoinsApplication.ViewModel
{
    public class CountryViewModel : SelectableViewModelBase<CountryModel>
    {
        public CountryViewModel(CountryModel model) 
            : base(model)
        {
        }
    }
}