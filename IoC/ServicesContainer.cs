using System;
using Data.Abstraction;
using Data.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using ToDo.Data;

namespace IoC
{
    public static class ServicesContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {    
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            var assembly = AppDomain.CurrentDomain.Load("Services");
            services.AddMediatR(assembly);
            services.AddDbContext<DataContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;

        }
    }
}