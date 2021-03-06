using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSMLicensing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LicenseSignatureServiceHandshake
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static Settings GeneralSettings { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var config = new LSMLicensing.Settings();
            Configuration.Bind("Properties", config);
            GeneralSettings = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(SocketConnector.Connected ? "Socket Connection provided!" : "Socket Connection still in progress!");
                    await SocketConnector.Connect();
                });
            });
        }
    }
}
