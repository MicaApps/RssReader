using Microsoft.UI.Xaml.Controls;
using System;

namespace RssReader.Contracts.Services
{
    public interface INavigationService
    {
        event EventHandler? Navigated;

        Frame? Frame { get; set; }

        bool CanGoBack { get; }

        bool GoBack();

        bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false);
    }
}