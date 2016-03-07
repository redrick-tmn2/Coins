using System;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel
{
    public class SelectableViewModelBase<T> : ViewModelBase
        where T : class
    {
        public T Model { get; set; }

        public event EventHandler IsSelectedChanged;

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                Set(ref _isSelected, value);

                IsSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public SelectableViewModelBase(T model)
        {
            Model = model;
            IsSelected = true;
        }
    }
}