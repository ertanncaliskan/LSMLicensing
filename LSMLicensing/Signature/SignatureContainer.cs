using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace LicenseRegistrationAPI.Signature
{
    public static class SignatureContainer
    {
        private static Dictionary<string, SignatureReference> _signatureDictionary = new Dictionary<string, SignatureReference>();
        public static bool CreateSignatureRequest(string licenseKey) 
        {
            if (!_signatureDictionary.ContainsKey(licenseKey)) 
            {
                var signal = new ManualResetEventSlim();
                _signatureDictionary[licenseKey] = new SignatureReference(signal);
                return true;
            }
            return false;
        }
        public static void SetSignature(string licenseKey, string signature) 
        {
            _signatureDictionary[licenseKey].LicenseSignature = signature;
        }
        public static string RunSignatureRequest(string licenseKey)
        {
            _signatureDictionary[licenseKey].Signal.Wait(Startup.GeneralSettings.SocketInterval);
            var signature = _signatureDictionary[licenseKey].LicenseSignature;
            _signatureDictionary.Remove(licenseKey);
            return signature;
        }
    }
}
