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
            //  services.AddScoped<ISecondPartOfPasswordService, SecondPartOfPasswordService>();
            services.AddScoped<ISecondPartOfPasswordService, CustomEncryptionService>();
            services.AddScoped<IRandomService, RandomService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            return services;
        }
    }
}
