using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseRegistrationAPI.Hubs;
using LicenseRegistrationAPI.Model;
using LicenseRegistrationAPI.Signature;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LicenseRegistrationAPI.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class LicenseController : Controller
    {
        private IHubContext<SignatureHub> signatureHubContext;
        private readonly ILogger<LicenseController> _logger;
        public LicenseController(ILogger<LicenseController> logger, IHubContext<SignatureHub> hubcontext)
        {
            _logger = logger;
            signatureHubContext = hubcontext;
        }
        [HttpGet]
        [Route("")]
        [Route("[action]")]
        public IActionResult RegistrationForm()
        {
            return View();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SaveRegistrationForm([FromBody] RegistrationModel model)
        {
            model.Validate();
            if (model.IsValid) 
            {
                bool created = SignatureContainer.CreateSignatureRequest(model.LicenseKey);
                if (created)
                {
                    await signatureHubContext.Clients.All.SendAsync("GenerateLicense", model.LicenseKey, JsonConvert.SerializeObject(model));
                    model.License = SignatureContainer.RunSignatureRequest(model.LicenseKey);
                    model.Validate();
                }
            }
            return Json(model);
        }
    }
}