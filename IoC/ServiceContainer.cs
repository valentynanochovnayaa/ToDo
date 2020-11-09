using System;
using Data.Abstraction;
using Data.Data;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using ToDo.Data;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Service.Abstraction.Dxos;
using Services.Dxos;

namespace IoC
{
    public static class ServicesContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {    
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IToDoUserDxo, ToDoUserDxo>();
            var assembly = AppDomain.CurrentDomain.Load("Services");
            services.AddMediatR(assembly);
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<DataContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
            
            return services;

        }
    }
}