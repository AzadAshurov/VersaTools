﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Infrastructure.Implementations.Services;

namespace VersaTools.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
       services.AddScoped<IAiService, AiService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            return services;
        }
    }
}
