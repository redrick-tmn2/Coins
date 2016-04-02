using System.Windows;
using CoinsApplication.Views;

namespace CoinsApplication.Models
{
    public class WindowCommandContext : DependencyObject
    {
        public IMainWindow Window
        {
            get { return (IMainWindow)GetValue(WindowProperty); }
            set { SetValue(WindowProperty, value); }
        }
        
        public static readonly DependencyProperty WindowProperty =
            DependencyProperty.Register(nameof(Window), typeof(IMainWindow), typeof(WindowCommandContext), new PropertyMetadata(null));

        public object Parameter
        {
            get { return GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }
        
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register(nameof(Parameter), typeof(object), typeof(WindowCommandContext), new PropertyMetadata(null));

        public WindowCommandContext()
        {
            Window = Application.Current.MainWindow as IMainWindow;
        }
    }
}