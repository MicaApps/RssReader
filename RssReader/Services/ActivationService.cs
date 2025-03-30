using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RssReader.Contracts.Services;
using RssReader.Helpers;
using RssReader.ViewModels;
using System;
using System.Threading.Tasks;

namespace RssReader.Activation
{
    public class ActivationService : IActivationService
    {
        private readonly IPageService _pageService;
        private readonly INavigationService _navigationService;

        private readonly Window _window;

        public ActivationService(Window window, IPageService pageService, INavigationService navigationService)
        {
            _window = window;
            _pageService = pageService;
            _navigationService = navigationService;
        }

        public async Task ActivateAsync(object activationArgs)
        {
            if (IsFirstRun())
            {
                NavigateTo(typeof(WelcomeViewModel).FullName!);
            }
            else
            {
                NavigateTo(typeof(HomeViewModel).FullName!);
            }

            if (App.Current is App app && app.Services.GetService<MainWindow>() is MainWindow mainWindow)
            {
                mainWindow.Activate();
            }

            await Task.CompletedTask;
        }

        private bool IsFirstRun()
        {
            // 实现首次运行检查逻辑
            return false;
        }

        private void NavigateTo(string pageKey, object? parameter = null)
        {
            _navigationService.NavigateTo(pageKey, parameter);
        }
    }
}