using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSearch.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace HotelSearch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "HotelSearch", Version = "v1"});
            });
            services.AddMemoryCache();
            services.AddHttpClient("auth-client", c =>
            {
                c.BaseAddress = new Uri("https://test.api.amadeus.com/v1/security/oauth2/");
                // c.DefaultRequestHeaders.Add("Authorization", "Bearer " + DateTime.Now);
            });
            services.AddHttpClient("hotel-search", c =>
            {
                c.BaseAddress = new Uri("https://test.api.amadeus.com/v2/shopping/");
                // c.DefaultRequestHeaders.Add("Authorization", "Bearer " + DateTime.Now);
            });
            
            services.Configure<HotelSearchApiSettings>(Configuration.GetSection(nameof(HotelSearchApiSettings)));
            services.AddSingleton<AuthTokenOptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelSearch v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}