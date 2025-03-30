using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using RssReader.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;

namespace RssReader
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static Window CurrentWindow => _window ?? (_window = new Window());
        private static Window? _window;

        private Mutex? _singleInstanceMutex;

        public IServiceProvider Services { get; }

        public App()
        {
            InitializeComponent();

            Services = ConfigureServices();

            UnhandledException += App_UnhandledException;

            EnsureSingleInstance();
        }

        private void EnsureSingleInstance()
        {
            _singleInstanceMutex = new Mutex(true, "RssReaderSingleInstanceMutex", out bool isNewInstance);
            if (!isNewInstance)
            {
                // 如果已经有实例在运行，退出当前实例
                _singleInstanceMutex = null;
                Current.Exit();
            }
        }

        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // 处理未处理的异常
        }

        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            _window.ExtendContentToTitleBar();
            _window.Activate();

            var activationService = Services.GetService<IActivationService>();
            await activationService!.ActivateAsync(args);
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // 服务注册
            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationViewService, NavigationViewService>();
            services.AddSingleton<IShareService, ShareService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<WindowHelper>();

            // 视图模型注册
            services.AddTransient<MainViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<ArticleDetailViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<WelcomeViewModel>();
            services.AddTransient<ReadLaterViewModel>();

            // 页面注册
            services.AddTransient<MainWindow>();
            services.AddTransient<HomePage>();
            services.AddTransient<ArticleDetailPage>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<WelcomePage>();
            services.AddTransient<ReadLaterPage>();

            return services.BuildServiceProvider();
        }
    }
}
