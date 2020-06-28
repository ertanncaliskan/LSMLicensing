using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseSignatureService.Model
{
    public class RegistrationModel
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string LicenseKey { get; set; }
    }
}
