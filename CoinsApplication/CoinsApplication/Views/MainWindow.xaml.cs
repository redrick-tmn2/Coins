using System.Windows;
using System.Windows.Controls;

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
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            EditFlyout.IsOpen = true;
        }
    }
}
