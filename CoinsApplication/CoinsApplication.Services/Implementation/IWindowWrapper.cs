using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace CoinsApplication.Services.Implementation
{
    public interface IWindowWrapper
    {
        Task<MessageDialogResult> ShowMessageAsync(string title, string message, DialogStyle dialogStyle);
    }
}