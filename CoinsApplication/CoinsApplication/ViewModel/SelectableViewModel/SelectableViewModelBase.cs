using System;
using CoinsApplication.DAL.Infrastructure;
using GalaSoft.MvvmLight;

namespace CoinsApplication.ViewModel.SelectableViewModel
{
    public class SelectableViewModelBase<T> : ViewModelBase
        where T : class, IEntity
    {
        public Entity<T> Model { get; set; }

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
            Model = null;
        }

        public SelectableViewModelBase(Entity<T> model)
        {
            Model = model;
            IsSelected = true;
        }

        public bool IsPassed(Entity<T> model)
        {
            if (!IsSelected)
            {
                return false;
            }

            return model == Model;
        }

        public static SelectableViewModelBase<T> Empty { get; } = new SelectableViewModelBase<T>();
    }
}