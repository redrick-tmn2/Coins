using System.ComponentModel;
using System.Threading.Tasks;
using CoinsApplication.Services.Interfaces.Window;

namespace CoinsApplication.Services.Interfaces
{
    public interface IWindowWrapper
    {
        Task<DialogResult> ShowMessageAsync(string title, string message, DialogStyle dialogStyle, DialogSettings dialogSettings = null);
        event CancelEventHandler Closing;
    }
}