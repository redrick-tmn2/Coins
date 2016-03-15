using System;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel.SelectableViewModel
{
    public class SelectableViewModelBase<T> : ViewModelBase
        where T : class, new ()
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
        private SelectableViewModelBase()
        {
            IsSelected = true;
            Model = new T();
        }

        public SelectableViewModelBase(T model)
        {
            Model = model;
            IsSelected = true;
        }

        public static SelectableViewModelBase<T> Empty { get; } = new SelectableViewModelBase<T>();
    }
}