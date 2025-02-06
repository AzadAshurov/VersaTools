using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VersaTools.Domain.Entitities.Identity;
using VersaTools.Persistence.ServiceRegistration;
using Microsoft.AspNetCore.Authentication;
using System;
using VersaTools.Persistence.DAL;
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
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

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