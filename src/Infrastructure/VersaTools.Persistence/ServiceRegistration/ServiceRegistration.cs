
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using VersaTools.Domain.Entitities.Identity;
using VersaTools.Persistence.DAL;

namespace VersaTools.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine("🔹 AddPersistenceServices is being executed!");
            services
                .AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("Default"))
                );


            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            
            services.AddScoped<IAuthenticationService, AuthenticationService>();

        
           



            return services;
        }
    }
}
