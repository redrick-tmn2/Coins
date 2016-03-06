using System.Windows;
using CoinsApplication.Services.Interfaces;
using Microsoft.Win32;

namespace CoinsApplication.Services.Implementation.System
{
    public class DialogService : IDialogService
    {
        private const string DefaultMessageBoxCaption = "Coins collection";

        private string OpenFileDialogAndGetFileName()
        {
            var openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return null;
        }

        public string ShowOpenFileDialog()
        {
            return OpenFileDialogAndGetFileName();
        }

        public MessageBoxResult ShowMessageBox(string text)
        {
            return MessageBox.Show(text, DefaultMessageBoxCaption);
        }
    }
}