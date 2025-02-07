﻿
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Persistence.DAL;
using VersaTools.Persistence.Implementations.Repositories;
using VersaTools.Persistence.Implementations.Repositories.Generic;
using VersaTools.Persistence.Implementations.Services;
using AuthenticationService = VersaTools.Persistence.Implementations.Services.AuthenticationService;

namespace VersaTools.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine(" AddPersistenceServices is being executed!");
            services
                .AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("Default"))
                );

            services.AddScoped<Application.Abstractions.Services.IAuthenticationService, AuthenticationService>();

            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
