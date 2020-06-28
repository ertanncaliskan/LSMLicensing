using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LicenseRegistrationAPI.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using LicenseRegistrationAPI.Views;
using FluentValidation.AspNetCore;
using LicenseRegistrationAPI.Model;
using System.Diagnostics;
using LSMLicensing;
using Microsoft.AspNetCore.Rewrite;

namespace LicenseRegistrationAPI
{
    public class Startup
    {
        public static Settings GeneralSettings { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var config = new Settings();
            Configuration.Bind("Properties", config);
            GeneralSettings = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                    .AddViews(options => {
                        options.ViewEngines.Insert(0, new BasicViewEngine());
                    }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegistrationModelValidator>()); ;
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, SocketTokenRequirementHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SocketToken",
                    policy => policy.Requirements.Add(new SocketTokenRequirement()));
            });
            services.AddSignalR();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignatureHub>("/signhub");
            });
            app.UseRewriter(new RewriteOptions().AddRewrite("/", "/License/RegistrationForm", true));
        }
    }
}
