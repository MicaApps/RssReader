using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using RssReader.Contracts.Services;
using RssReader.Helpers;
using System;

namespace RssReader.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IPageService _pageService;
        private Frame? _frame;

        public event EventHandler? Navigated;

        public Frame? Frame
        {
            get
            {
                if (_frame == null)
                {
                    _frame = WindowHelper.GetWindowContent() as Frame;
                    RegisterFrameEvents();
                }

                return _frame;
            }
            set
            {
                UnregisterFrameEvents();
                _frame = value;
                RegisterFrameEvents();
            }
        }

        public bool CanGoBack => Frame?.CanGoBack == true;

        public NavigationService(IPageService pageService)
        {
            _pageService = pageService;
        }

        public bool GoBack()
        {
            if (CanGoBack)
            {
                Frame!.GoBack();
                return true;
            }

            return false;
        }

        public bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false)
        {
            var pageType = _pageService.GetPageType(pageKey);

            if (Frame != null)
            {
                var navigationResult = Frame.Navigate(pageType, parameter);
                if (navigationResult && clearNavigation)
                {
                    Frame.BackStack.Clear();
                }

                if (navigationResult)
                {
                    Navigated?.Invoke(this, EventArgs.Empty);
                }

                return navigationResult;
            }

            return false;
        }

        private void RegisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated += OnNavigated;
            }
        }

        private void UnregisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated -= OnNavigated;
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            Navigated?.Invoke(this, EventArgs.Empty);
        }
    }
}
