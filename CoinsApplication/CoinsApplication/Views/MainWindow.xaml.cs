using System.Windows;
using CoinsApplication.Services.Implementation;
using CoinsApplication.ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CoinsApplication.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            ((MainWindowViewModel)DataContext).Window = new WindowWrapper(this);
        }
    }
}
