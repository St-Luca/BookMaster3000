using Application;
using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.interfaces;
using Persistence.interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        MapsterConfig.RegisterMappings();
        
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();

        builder.Services.AddScoped<IClientCardService, ClientCardService>();
        builder.Services.AddScoped<IClientCardRepository, ClientCardRepository>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<IExhibitionService, ExhibitionService>();
        builder.Services.AddScoped<IExhibitionRepository, ExhibitionRepository>();

        builder.Services.AddScoped<IExhibitionBookRepository, ExhibitionBookRepository>();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "http://localhost:5604")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        builder.Services.AddAuthorization();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
            };
        });

        builder.Services.AddControllers();
        builder.Services.AddDbContext<LibraryContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Host=postgres.db;Database=bookmaster_DB;Username=postgres;Password=1234")));


        var app = builder.Build();

        app.UseCors("AllowSpecificOrigins");

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookMaster3000 API V1");
        });

        app.UseAuthorization();
        app.MapControllers();
        app.UseHttpsRedirection();

        app.Run();
    }
}
