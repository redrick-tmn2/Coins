using CoinsApplication.Models;

namespace CoinsApplication.ViewModel
{
    public class CurrencyViewModel : SelectableViewModelBase<CurrencyModel>
    {
        public CurrencyViewModel(CurrencyModel model) 
            : base(model)
        {
        }
    }
}
