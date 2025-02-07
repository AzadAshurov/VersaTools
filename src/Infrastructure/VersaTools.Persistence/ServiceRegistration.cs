using Microsoft.Extensions.DependencyInjection;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Persistence.Implementations.Services;

namespace VersaTools.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
} 