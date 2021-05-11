using Microsoft.Extensions.DependencyInjection;
using RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase;
using RussiaPublicHealthProtection.ApplicationServices.Ports.Cache;
using RussiaPublicHealthProtection.ApplicationServices.Repositories;
using RussiaPublicHealthProtection.DesktopClient.InfrastructureServices.ViewModels;
using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.DomainObjects.Ports;
using RussiaPublicHealthProtection.InfrastructureServices.Cache;
using RussiaPublicHealthProtection.InfrastructureServices.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RussiaPublicHealthProtection.DesktopClient 
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDomainObjectsCache<PublicHealthProtection>, DomainObjectsMemoryCache<PublicHealthProtection>>();
            services.AddSingleton<NetworkPublicHealthProtectionRepository>(
                x => new NetworkPublicHealthProtectionRepository("localhost", 80, useTls: false, x.GetRequiredService<IDomainObjectsCache<PublicHealthProtection>>())
            );
            services.AddSingleton<CachedReadOnlyPublicHealthProtectionRepository>(
                x => new CachedReadOnlyPublicHealthProtectionRepository(
                    x.GetRequiredService<NetworkPublicHealthProtectionRepository>(),
                    x.GetRequiredService<IDomainObjectsCache<PublicHealthProtection>>()
                )
            );
            services.AddSingleton<IReadOnlyPublicHealthProtectionRepository>(x => x.GetRequiredService<CachedReadOnlyPublicHealthProtectionRepository>());
            services.AddSingleton<IGetPublicHealthProtectionListUseCase, GetPublicHealthProtectionListUseCase>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs args)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
