using LicenseRegistrationAPI.Signature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LicenseRegistrationAPI.Hubs
{
    [Authorize("SocketToken")]
    public class SignatureHub : Hub
    {
        public async Task SendMessage(string licenseKey, string signature)
        {
            SignatureContainer.SetSignature(licenseKey, signature);
        }
    }
}

