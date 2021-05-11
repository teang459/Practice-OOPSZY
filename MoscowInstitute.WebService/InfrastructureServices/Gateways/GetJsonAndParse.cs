using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.InfrastructureServices.Gateways.Database;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace RussiaPublicHealthProtection.WebService.InfrastructureServices.Gateways
{
    public class GetJsonAndParse
    {
        public async Task ParseAndPush()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string result = client.DownloadString("https://apidata.mos.ru/v1/datasets/1185/rows?$top=1000&api_key=c941a998bbb9e1e374fc2d7a33f61ed0");
            List<ResultFromServer> resultServer = JsonConvert.DeserializeObject<List<ResultFromServer>>(result);
            var optionsBuilder = new DbContextOptionsBuilder<PublicHealthProtectionContext>();
            string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\"));
            string newnewpath = System.IO.Path.Combine(newPath, "MoscowInstitute.WebService", "RussiaPublicHealthProtection.db");
            optionsBuilder.UseSqlite($"Data Source={newnewpath}");
            var context = new PublicHealthProtectionContext(options: optionsBuilder.Options);
            context.Database.ExecuteSqlRaw("DELETE FROM TrafficRestrictions");
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
            await Task.CompletedTask;
        }
    }
}
