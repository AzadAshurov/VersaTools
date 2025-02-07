
using System.Text;

using Microsoft.IdentityModel.Tokens;

using VersaTools.Persistence.ServiceRegistration;
using Microsoft.AspNetCore.Authentication;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddPersistenceServices(builder.Configuration);
        //builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        //    builder.Services.AddIdentity<User, IdentityRole>()
        //.AddEntityFrameworkStores<AppDbContext>()
        //.AddDefaultTokenProviders();

        //    builder.Services.AddIdentity<User, IdentityRole>()
        //.AddEntityFrameworkStores<DbContext>()
        // .AddDefaultTokenProviders();
      
       

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