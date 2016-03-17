using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using CoinsApplication.Services.Utils;

namespace CoinsApplication.Services.Implementation
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

        public Task<MessageDialogResult> ShowMessageAsync(string title, string message, DialogStyle messageDialogStyle)
        {
            return _metroWindow.ShowMessageAsync(title, message, messageDialogStyle.ToMessageDialogStyle());
        }
    }
}
