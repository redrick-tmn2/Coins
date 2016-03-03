using CoinsApplication.Services.Interfaces;
using Microsoft.Win32;

namespace CoinsApplication.Services.Implementation.System
{
    public class DialogService : IDialogService
    {
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

        public string OpenFileDialog()
        {
            return OpenFileDialogAndGetFileName();
        }
    }
}