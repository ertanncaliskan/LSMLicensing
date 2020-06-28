using LicenseSignatureService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicenseSignatureService.Logic
{
    public static class LicenseLogic
    {
        public static string ConnectionString { get; set; }
        public static string GenerateLicense(RegistrationModel model) 
        {
            using (var db = new LicenseContext(ConnectionString))
            {
                var license = db.LicenseRegistries
                    .Where(x => x.LicenseKey == model.LicenseKey)
                    .FirstOrDefault();
                if (license == null)
                    return "0";
                else if (!string.IsNullOrEmpty(license.LicenseSignature))
                    return "-1";

                license.LicenseSecret = JsonConvert.SerializeObject(model).GetHashCode().ToString();
                string signedLicense = Convert.ToBase64String(Encoding.UTF8.GetBytes(license.LicenseSecret + license.LicenseKey));
                signedLicense = signedLicense.Replace("=", "");
                signedLicense = signedLicense.Replace("+", "");
                license.LicenseSignature = signedLicense;
                db.SaveChanges();
                return license.LicenseSignature;
            }
        }
    }
}
