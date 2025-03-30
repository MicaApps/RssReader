using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RssReader.Contracts.Services;
using System;
using System.Threading.Tasks;

namespace RssReader.Services
{
    public class DialogService : IDialogService
    {
        public async Task<ContentDialogResult> ShowDialogAsync(string title, string content, string primaryButtonText, string secondaryButtonText)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                PrimaryButtonText = primaryButtonText,
                SecondaryButtonText = secondaryButtonText,
                XamlRoot = App.CurrentWindow.Content.XamlRoot
            };

            return await dialog.ShowAsync();
        }
    }
}