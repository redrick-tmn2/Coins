using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CoinsApplication.Services.Interfaces;
using CoinsApplication.Services.Interfaces.Window;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CoinsApplication.Services.Window
{
    public class WindowWrapper : IWindowWrapper
    {
        private readonly MetroWindow _metroWindow;

        public WindowWrapper(MetroWindow metroWindow)
        {
            if (metroWindow == null)
                throw new ArgumentNullException(nameof(metroWindow));

            _metroWindow = metroWindow;
        }

        public event CancelEventHandler Closing
        {
            add { _metroWindow.Closing += value; }
            remove { _metroWindow.Closing += value; }
        }

        public Task<DialogResult> ShowMessageAsync(string title, string message, DialogStyle dialogStyle, DialogSettings dialogSettings)
        {
            var dialogResult = _metroWindow.ShowMessageAsync(title, message, dialogStyle.ToMessageDialogStyle(), dialogSettings.ToMetroDialogSettings());

            return dialogResult.ContinueWith(x => x.Result.ToDialogResult());
        }
    }
}
