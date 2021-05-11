using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.DomainObjects.Ports;
using RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase;
using RussiaPublicHealthProtection.ApplicationServices.Ports.Gateways.Database;
using RussiaPublicHealthProtection.InfrastructureServices.Gateways.Database;
using Microsoft.EntityFrameworkCore;
using RussiaPublicHealthProtection.ApplicationServices.Repositories;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using RussiaPublicHealthProtection.WebService.InfrastructureServices.Gateways;
using RussiaPublicHealthProtection.WebService.Scheduler;

namespace RussiaPublicHealthProtection.WebService 
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PublicHealthProtectionContext>(opts =>
                opts.UseSqlite($"Filename={System.IO.Path.Combine(System.Environment.CurrentDirectory, "RussiaPublicHealthProtection.db")}")
            );
            services.AddHostedService<ScheduleTask>();
            services.AddScoped<IPublicHealthProtectionDatabaseGateway, PublicHealthProtectionEFSqliteGateway>();

            services.AddScoped<DbPublicHealthProtectionRepository>();
            services.AddScoped<IReadOnlyPublicHealthProtectionRepository>(x => x.GetRequiredService<DbPublicHealthProtectionRepository>());
            services.AddScoped<IPublicHealthProtectionRepository>(x => x.GetRequiredService<DbPublicHealthProtectionRepository>());

            services.AddScoped<IGetPublicHealthProtectionListUseCase, GetPublicHealthProtectionListUseCase>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
