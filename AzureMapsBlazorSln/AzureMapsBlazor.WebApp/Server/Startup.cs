using AzureMapsBlazor.WebApp.Server.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PTI.Microservices.Library.Interceptors;
using PTI.Microservices.Library.Services;
using System.Linq;

namespace AzureMapsBlazor.WebApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AzureMapsConfiguration azureMapsConfiguration = this.Configuration.GetSection("AzureMapsConfiguration")
                .Get<AzureMapsConfiguration>();
            services.AddSingleton(azureMapsConfiguration);
            var azuremapsServiceConfig =
                new PTI.Microservices.Library.Configuration.AzureMapsConfiguration()
                {
                    Key = azureMapsConfiguration.SubscriptionKey
                };
            PTI.Microservices.Library.Configuration.GlobalPackageConfiguration.RapidApiKey = "a3893edcbfmsh2efa1861dcc7a10p159864jsnf17e667d1bf7";
            services.AddSingleton(azuremapsServiceConfig);
            services.AddTransient<CustomHttpClientHandler>();
            services.AddTransient<CustomHttpClient>();
            services.AddTransient<AzureMapsService>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
