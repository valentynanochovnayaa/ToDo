using AutoMapper;
using Data.Data;
using Domain.Entities;
using Domain.Settings;
using IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDo.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services.Services;
using ToDo.ExtensionMethods;

namespace ToDo
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostEnvironment _currentEnv;
        public Startup(IConfiguration configuration, IHostEnvironment currentEnv)
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{currentEnv.EnvironmentName}.json", true)
                .Build();
            _currentEnv = currentEnv;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServices(_config);
            services.AddControllersWithViews();
            services.AddIdentity<User, Role>(config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequiredLength = 7;
                })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            if (_currentEnv.IsEnvironment("Testing"))
            {
                services.AddTokenAuthentication(_config);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("TestingDB");
                });
            }
            else
            {
                services.AddTokenAuthentication(_config);
                services.AddDbContext<DataContext>(options => {
                    options.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
                });

            }
            // In production, the Angular files will be served from this directory
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment() && !env.IsEnvironment("Testing"))
            {
                app.UseSpaStaticFiles();
            }

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new {error = exception.StackTrace});
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}