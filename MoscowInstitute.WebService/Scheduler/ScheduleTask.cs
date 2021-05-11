using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RussiaPublicHealthProtection.InfrastructureServices.Gateways.Database;
using RussiaPublicHealthProtection.WebService.InfrastructureServices.Gateways;
using RussiaPublicHealthProtection.WebService.Scheduler;
using System.IO;

namespace RussiaPublicHealthProtection.WebService.Scheduler
{
    public class ScheduleTask : ScheduledProcessor
    {

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "*/1 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string result = client.DownloadString("https://apidata.mos.ru/v1/datasets/1185/rows?$top=1000&api_key=c941a998bbb9e1e374fc2d7a33f61ed0");
            List<ResultFromServer> resultServer = JsonConvert.DeserializeObject<List<ResultFromServer>>(result);
            var optionsBuilder = new DbContextOptionsBuilder<PublicHealthProtectionContext>();
            string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
            string newnewpath = System.IO.Path.Combine(newPath, "MoscowInstitute.WebService", "RussiaPublicHealthProtection.db");
            optionsBuilder.UseSqlite($"Data Source={newnewpath}");
            var context = new PublicHealthProtectionContext(options: optionsBuilder.Options);
            context.Database.ExecuteSqlRaw("DELETE FROM PublicHealthProtections");
            using (context)
            {
                foreach (var item in resultServer)
                {
                    DomainObjects.PublicHealthProtection publicHealthProtection = new DomainObjects.PublicHealthProtection();

                    publicHealthProtection.LicenseHolderName = item.Cells.LicenseHolderName;
                    publicHealthProtection.global_id = item.Cells.global_id;
                    publicHealthProtection.INN = item.Cells.INN;
                    publicHealthProtection.OGRN = item.Cells.OGRN;
                    publicHealthProtection.License = item.Cells.License;
                    publicHealthProtection.PermissionList = item.Cells.PermissionList;
                    publicHealthProtection.Address = item.Cells.Address;
                    publicHealthProtection.RegistrationDate = item.Cells.RegistrationDate;
                    publicHealthProtection.LicenseAuthority = item.Cells.LicenseAuthority;
                    publicHealthProtection.LicensedObjectAddress = item.Cells.LicensedObjectAddress;

                    context.Entry(publicHealthProtection).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
        
            
            Console.WriteLine("Updated db.");
            return Task.CompletedTask;
        }
    }
}
