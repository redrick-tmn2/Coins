using System;
using CoinsApplication.Services.Interfaces.Window;
using MahApps.Metro.Controls.Dialogs;

namespace CoinsApplication.Services.Window
{
    public static class Extensions
    {
        public static MessageDialogStyle ToMessageDialogStyle(this DialogStyle dialogStyle)
        {
            switch (dialogStyle)
            {
                case DialogStyle.Affirmative : 
                    return MessageDialogStyle.Affirmative;
                case DialogStyle.AffirmativeAndNegative :
                    return MessageDialogStyle.AffirmativeAndNegative;
                case DialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary:
                    return MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary;
                case DialogStyle.AffirmativeAndNegativeAndSingleAuxiliary:
                    return MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary;
                default:
                    throw new InvalidOperationException($"{dialogStyle} is not supported");

            }
        }

        public static DialogResult ToDialogResult(this MessageDialogResult dialogResult)
        {
            switch (dialogResult)
            {
                case MessageDialogResult.Affirmative:
                    return DialogResult.Affirmative;
                case MessageDialogResult.FirstAuxiliary:
                    return DialogResult.FirstAuxiliary;
                case MessageDialogResult.Negative:
                    return DialogResult.Negative;
                case MessageDialogResult.SecondAuxiliary:
                    return DialogResult.SecondAuxiliary;
                default:
                    throw new InvalidOperationException($"{dialogResult} is not supported");

            }
        }

        public static MetroDialogSettings ToMetroDialogSettings(this DialogSettings dialogSettings)
        {
            if (dialogSettings == null)
            {
                return null;
            }

            return new MetroDialogSettings
            {
                AffirmativeButtonText = dialogSettings.AffirmativeButtonText,
                FirstAuxiliaryButtonText = dialogSettings.FirstAuxiliaryButtonText,
                NegativeButtonText = dialogSettings.NegativeButtonText,
                SecondAuxiliaryButtonText = dialogSettings.SecondAuxiliaryButtonText
            };
        }
    }
}
