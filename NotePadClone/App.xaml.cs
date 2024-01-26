using System.CodeDom;
using System.Configuration;
using System.Data;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using NotePadClone.Services;
using NotePadClone.ViewModels;

namespace NotePadClone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetService<MainWindow>() ?? throw new InvalidOperationException("Could not resolve MainWindow.");
            mainWindow.Show();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IDocumentService, DocumentService>();
            services.AddTransient<MainWindowVm>();
            services.AddTransient<MainWindow>();
        }
    }
}
