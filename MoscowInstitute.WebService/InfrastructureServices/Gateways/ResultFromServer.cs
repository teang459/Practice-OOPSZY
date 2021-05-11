using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.WebService.InfrastructureServices.Gateways
{
    public class Cells
    {

        public string LicenseHolderName { get; set; }

        public string global_id { get; set; }

        public string INN { get; set; }

        public string OGRN { get; set; }

        public string License { get; set; }

        public string PermissionList { get; set; }

        public string Address { get; set; }

        public string RegistrationDate { get; set; }

        public string LicenseAuthority { get; set; }

        public string LicensedObjectAddress { get; set; }


    }

    public class ResultFromServer
    {
        public int Number { get; set; }
        public Cells Cells { get; set; }
    }
}
