using System.Windows;

namespace CoinsApplication.Services.Interfaces
{
    public interface IDialogService
    {
        string ShowOpenFileDialog();

        MessageBoxResult ShowMessageBox(string text);
    }
}
