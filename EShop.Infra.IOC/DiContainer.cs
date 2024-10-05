using EShop.Application.Services.Implementations;
using EShop.Application.Services.Interfaces;
using EShop.Domain.Interfaces;
using EShop.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infra.IOC
{
    public static class DiContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailSender, EmailSender>();
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
