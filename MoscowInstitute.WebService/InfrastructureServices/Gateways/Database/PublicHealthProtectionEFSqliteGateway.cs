using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.ApplicationServices.Ports.Gateways.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using RussiaPublicHealthProtection.WebService.InfrastructureServices.Gateways;
using System.Text;
using Newtonsoft.Json;

namespace RussiaPublicHealthProtection.InfrastructureServices.Gateways.Database
{
    public class PublicHealthProtectionEFSqliteGateway : IPublicHealthProtectionDatabaseGateway
    {
        private readonly PublicHealthProtectionContext _publicHealthProtectionContext;

        public PublicHealthProtectionEFSqliteGateway(PublicHealthProtectionContext publicHealthProtectionContext)
            => _publicHealthProtectionContext = publicHealthProtectionContext;

        public async Task<PublicHealthProtection> GetPublicHealthProtection(long id)
           => await _publicHealthProtectionContext.PublicHealthProtections.Where(r => r.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<PublicHealthProtection>> GetAllPublicHealthProtection()
            => await _publicHealthProtectionContext.PublicHealthProtections.ToListAsync();

        public async Task<IEnumerable<PublicHealthProtection>> QueryPublicHealthProtection(Expression<Func<PublicHealthProtection, bool>> filter)
            => await _publicHealthProtectionContext.PublicHealthProtections.Where(filter).ToListAsync();

        public async Task AddPublicHealthProtection(PublicHealthProtection publicHealthProtection)
        {
            _publicHealthProtectionContext.PublicHealthProtections.Add(publicHealthProtection);
            await _publicHealthProtectionContext.SaveChangesAsync();
        }

        public async Task UpdatePublicHealthProtection(PublicHealthProtection publicHealthProtection)
        {
            _publicHealthProtectionContext.Entry(publicHealthProtection).State = EntityState.Modified;
            await _publicHealthProtectionContext.SaveChangesAsync();
        }

        public async Task RemovePublicHealthProtection(PublicHealthProtection publicHealthProtection)
        {
            _publicHealthProtectionContext.PublicHealthProtections.Remove(publicHealthProtection);
            await _publicHealthProtectionContext.SaveChangesAsync();
        }

        public async Task ParseAndPush()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string result = client.DownloadString("https://apidata.mos.ru/v1/datasets/1185/rows?$top=1000&api_key=c941a998bbb9e1e374fc2d7a33f61ed0");
            List<ResultFromServer> resultServer = JsonConvert.DeserializeObject<List<ResultFromServer>>(result);
            var optionsBuilder = new DbContextOptionsBuilder<PublicHealthProtectionContext>();
            optionsBuilder.UseSqlite("Data Source=C:/Users/sck/desktop/teang praktika toluk - copy/teang praktika/MoscowInstitute.WebService/MoscowTrafficRestriction.db"); ;
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
            await Task.CompletedTask;
        }
    }
}
