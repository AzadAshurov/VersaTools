using Microsoft.OpenApi.Models;
using Stripe;
using VersaTools.Application.ServiceRegistration;
using VersaTools.Infrastructure.ServiceRegistration;
using VersaTools.Persistence.DAL;
using VersaTools.Persistence.ServiceRegistration;

//creating new package
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {your token}'"
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
            new string[] {}
        }
    });
        });
        //builder.Services.AddAuthentication(opt =>
        //{
        //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //}).AddJwtBearer(opt =>
        //{
        //    opt.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,

        //        ValidIssuer = builder.Configuration["JWT:Issuer"],
        //        ValidAudience = builder.Configuration["JWT:Audience"],
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        //        LifetimeValidator = (notBefore, expired, token, param) => token != null ? expired > DateTime.UtcNow : false



        //    };
        //});
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();




        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();


        builder.Services.AddPersistenceServices(builder.Configuration)
                         .AddInfrastructureServices()
                         .AddApplicationServices();


        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
        StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }



        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        StripeConfiguration.ApiKey = app.Configuration.GetSection("Stripe")["SecretKey"];
        app.MapControllers();
        foreach (var endpoint in app.Urls)
        {
            Console.WriteLine($"Listening on: {endpoint}");
        }

        app.Run();
    }
}
