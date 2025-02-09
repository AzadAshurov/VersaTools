
using VersaTools.Infrastructure.ServiceRegistration;
using VersaTools.Persistence.ServiceRegistration;
using Microsoft.AspNetCore.Authentication;
using VersaTools.Application.ServiceRegistration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
 .AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = builder.Configuration["JWT:issuer"],
         ValidAudience = builder.Configuration["JWT:audience"],
         IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])
         ),
         ClockSkew = TimeSpan.Zero
     };
 });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
          
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Format: Bearer {your token}",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        builder.Services.AddPersistenceServices(builder.Configuration).AddInfrastructureServices().AddApplicationServices();

        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.Use(async (context, next) =>
        {
            var token = context.Request.Headers["Authorization"];
            Console.WriteLine($"Authorization Header: {token}");
            await next();
        });

        app.UseHttpsRedirection();
        app.UseAuthentication(); 
        app.UseAuthorization();  
        app.MapControllers();
        app.Run();
    }
}
