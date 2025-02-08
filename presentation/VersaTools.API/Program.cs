
using VersaTools.Infrastructure.ServiceRegistration;
using VersaTools.Persistence.ServiceRegistration;
using Microsoft.AspNetCore.Authentication;
using VersaTools.Application.ServiceRegistration;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddPersistenceServices(builder.Configuration).AddInfrastructureServices().AddApplicationServices(); 
      
       

        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        //.AddApplicationServices();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}