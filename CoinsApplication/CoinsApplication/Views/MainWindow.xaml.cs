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

        private void ListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Flyout1.IsOpen = true;
        }
    }
}
