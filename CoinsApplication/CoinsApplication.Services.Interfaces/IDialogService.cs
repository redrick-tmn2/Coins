namespace CoinsApplication.Services.Interfaces
{
    public interface IDialogService
    {
        string ShowOpenFileDialog();

        string ShowOpenFileDialog(string filter);
    }
}
