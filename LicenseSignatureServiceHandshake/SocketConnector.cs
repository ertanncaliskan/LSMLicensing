using LicenseSignatureService.Logic;
using LicenseSignatureService.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSignatureServiceHandshake
{
    public static class SocketConnector
    {
        static HubConnection connection;
        public static bool Connected { get; private set; }
        static bool connectionRequested = false;
        public static async Task Connect() 
        {
            if (!connectionRequested)
            {
                string _myAccessToken = Startup.GeneralSettings.SocketSecret;
                connection = new HubConnectionBuilder()
                    .WithUrl(Startup.GeneralSettings.SocketUrl + _myAccessToken, options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(_myAccessToken);
                    })
                    .Build();
                connection.Closed += async (error) =>
                {
                    Connected = false;
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await tryUntilConnected();
                };
                connection.On<string, string>("GenerateLicense", async (licenseKey, registrationContent) =>
                {
                    LicenseLogic.ConnectionString = Startup.GeneralSettings.DatabaseConnection;
                    var signedLicense = LicenseLogic.GenerateLicense(JsonConvert.DeserializeObject<RegistrationModel>(registrationContent));
                    await connection.InvokeAsync("SendMessage", licenseKey, signedLicense);
                });
                await tryUntilConnected();
            }
            connectionRequested = true;
        }

        private static async Task tryUntilConnected() {
            while (!Connected)
            {
                try
                {
                    await connection.StartAsync();
                    Connected = true;
                }
                catch (Exception ex) 
                {
                    
                    if (!(ex.InnerException is SocketException socketEx && socketEx != null &&
                        (socketEx.SocketErrorCode == SocketError.ConnectionRefused || socketEx.SocketErrorCode == SocketError.TimedOut)))
                        throw ex;
                }
            }
        }
    }
}
