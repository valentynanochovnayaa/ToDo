using System;
using Data.Abstraction;
using Data.Data;
using Domain.DTO;
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
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            services.AddScoped<IToDoItemDxo, ToDoItemDxo>();
            services.AddScoped<IToDoUserDxo, ToDoUserDxo>();
            var assembly = AppDomain.CurrentDomain.Load("Services");
            services.AddMediatR(assembly);
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
            var cong = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
            
            return services;

        }
    }
}