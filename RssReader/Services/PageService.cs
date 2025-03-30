using RssReader.Contracts.Services;
using RssReader.Views;
using System;
using System.Collections.Generic;
using Windows.UI.ApplicationSettings;

namespace RssReader.Services
{
    public class PageService : IPageService
    {
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>
        {
            { typeof(HomeViewModel).FullName!, typeof(HomePage) },
            { typeof(ArticleDetailViewModel).FullName!, typeof(ArticleDetailPage) },
            { typeof(SettingsViewModel).FullName!, typeof(SettingsPage) },
            { typeof(WelcomeViewModel).FullName!, typeof(WelcomePage) }
        };

        public Type GetPageType(string key)
        {
            if (_pages.ContainsKey(key))
            {
                return _pages[key];
            }

            throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
        }
    }
}