using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LicenseRegistrationAPI.Signature
{
    public class SignatureReference
    {
        public SignatureReference(ManualResetEventSlim signal)
        {
            Signal = signal;
        }
        public ManualResetEventSlim Signal { get; set; }
        private string _licenseSignature { get; set; }
        public string LicenseSignature
        {
            get
            {
                return _licenseSignature;
            }
            set
            {
                _licenseSignature = value;
                Signal.Set();
            }
        }
    }
}
