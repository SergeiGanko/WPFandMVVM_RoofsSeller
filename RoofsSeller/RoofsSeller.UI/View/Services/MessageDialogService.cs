﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace RoofsSeller.UI.View.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        private MetroWindow MetroWindow => (MetroWindow)App.Current.MainWindow;

        public async Task ShowInfoDialogAsync(string text)
        {
            await MetroWindow.ShowMessageAsync("Attention", text);
        }

        public async Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title)
        {
            var result = await MetroWindow.ShowMessageAsync(title, text, MessageDialogStyle.AffirmativeAndNegative);
            return result ==  MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative
                ? MessageDialogResult.Ok
                : MessageDialogResult.Cancel;
        }
    }

    public enum MessageDialogResult
    {
        Ok,
        Cancel
    }
}
