using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaPublicHealthProtection.DomainObjects
{
    public class PublicHealthProtection : DomainObject 
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
}
