using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Persistence.DAL;

namespace VersaTools.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("Default"))
                );

           
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            return services;
        }
    }
}
