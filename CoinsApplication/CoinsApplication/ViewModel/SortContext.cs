using CoinsApplication.Extensions;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel
{
    public class SortContext : ObservableObject
    {
        private Order _order;
        public Order Order
        {
            get { return _order; }
            set { Set(ref _order, value); }
        }

        public void ToggleOrder()
        {
            Order = EnumExtensions.Next(Order);
        }
    }
}