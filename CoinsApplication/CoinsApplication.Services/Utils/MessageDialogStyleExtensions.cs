using System;
using CoinsApplication.Services.Implementation;
using MahApps.Metro.Controls.Dialogs;

namespace CoinsApplication.Services.Utils
{
    public static class MessageDialogStyleExtensions
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
    }
}
