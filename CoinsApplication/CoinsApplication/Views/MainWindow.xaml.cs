using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CoinsApplication.Models;
using CoinsApplication.Services.Interfaces.DirtySerializing;
using CoinsApplication.Services.Interfaces.Logging;
using MahApps.Metro.Controls.Dialogs;

namespace CoinsApplication.Views
{
    public interface IMainWindow
    {
        Task<MessageDialogResult> ThrowValidationFailedMessageBox(IEnumerable<string> invalidCoins);
        void ThrowUnknownErrorMessageBox(Exception ex);
        void ThrowUnableToLoadImageErrorMessageBox(Exception ex);
        Task<MessageDialogResult> ThrowWillBeRemovedMessageBox(CoinModel coin);
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainWindow
    {
        private readonly ILoggingService _loggingService;
        private readonly IDirtySerializableCacheService _serializableService;

        public MainWindow(ILoggingService loggingService, IDirtySerializableCacheService serializableService)
        {
            _loggingService = loggingService;
            _serializableService = serializableService;

            InitializeComponent();
        }

        public async Task<MessageDialogResult> ThrowValidationFailedMessageBox(IEnumerable<string> invalidCoins)
        {
            var invalidCoinsMessage = Environment.NewLine + string.Join(Environment.NewLine, invalidCoins);

            var message = string.Format(Properties.Resources.ValidationFailedMessageBoxText, invalidCoinsMessage);
            
            return await this.ShowMessageAsync(Properties.Resources.ValidationFailedMessageBoxTitle
                , message);
        }

        public async void ThrowUnknownErrorMessageBox(Exception ex)
        {
            _loggingService.Error(Properties.Resources.UnknownErrorMessageBoxTitle, ex);
            await this.ShowMessageAsync(Properties.Resources.UnknownErrorMessageBoxTitle
                , string.Format(Properties.Resources.UnknownErrorMessageBoxText, ex.Message));
        }

        public async void ThrowUnableToLoadImageErrorMessageBox(Exception ex)
        {
            _loggingService.Error(Properties.Resources.UnknownErrorMessageBoxTitle, ex);
            await this.ShowMessageAsync(Properties.Resources.UnableToLoadImageErrorMessageBoxTitle
                , string.Format(Properties.Resources.UnableToLoadImageErrorMessageBoxText, ex.Message));
        }

        public async Task<MessageDialogResult> ThrowWillBeRemovedMessageBox(CoinModel coin)
        {
            var message = string.Format(Properties.Resources.RemoveCoinMessageBoxText, coin.Title);
            return await this.ShowMessageAsync(Properties.Resources.RemoveCoinMessageBoxTtitle
                , message
                , MessageDialogStyle.AffirmativeAndNegative
                , new MetroDialogSettings
                {
                    AffirmativeButtonText = "Yes",
                    NegativeButtonText = "No"
                });
        }

        private void OnClosedHandler(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
