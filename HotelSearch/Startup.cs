using System;
using HotelSearch.Authentication;
using HotelSearch.HotelSearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;

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
            services.AddHttpClient("auth-client",
                    c => { c.BaseAddress = new Uri("https://test.api.amadeus.com/v1/security/oauth2/"); })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500)));

            services.AddHttpClient("hotel-search",
                c => { c.BaseAddress = new Uri("https://test.api.amadeus.com/v2/shopping/"); }).
                AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500)));

            services.Configure<HotelSearchApiSettings>(Configuration.GetSection(nameof(HotelSearchApiSettings)));
            services.AddSingleton<IAuthService, AuthService>();
            services.AddScoped<IHotelSearchService, HotelSearchService>();
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