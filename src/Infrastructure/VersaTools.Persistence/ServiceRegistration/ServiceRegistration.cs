using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Domain.Entitities;
using VersaTools.Persistence.DAL;
using VersaTools.Persistence.Implementations.Repositories;
using VersaTools.Persistence.Implementations.Repositories.Generic;
using VersaTools.Persistence.Implementations.Services;


namespace VersaTools.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            Console.WriteLine(" AddPersistenceServices is being executed!");
            services
               .AddDbContext<AppDbContext>(opt =>
               opt.UseSqlServer(configuration.GetConnectionString("Default"),
               m => m.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
               ));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
  .AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddScoped<IComplaintService, ComplaintService>();

            services.AddScoped<IResponseRepository, ResponseRepository>();
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
