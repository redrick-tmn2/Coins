using System.ComponentModel;
using System.Threading.Tasks;

namespace CoinsApplication.Services.Interfaces.Window
{
    public interface IWindowWrapper
    {
        Task<DialogResult> ShowMessageAsync(string title, string message, DialogStyle dialogStyle, DialogSettings dialogSettings = null);
        event CancelEventHandler Closing;
    }
}