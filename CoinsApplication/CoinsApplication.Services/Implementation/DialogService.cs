using CoinsApplication.Services.Interfaces;
using Microsoft.Win32;

namespace CoinsApplication.Services.Implementation
{
    public class DialogService : IDialogService
    {
        private string OpenFileDialogAndGetFileName(string filter)
        {
            var openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = filter,
            };

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return null;
        }

        public string ShowOpenFileDialog(string filter)
        {
            return OpenFileDialogAndGetFileName(filter);
        }

        public string ShowOpenFileDialog()
        {
            return OpenFileDialogAndGetFileName(string.Empty);
        }
    }
}