using Application;
using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IClientCardService, ClientCardService>();
builder.Services.AddScoped<IClientCardRepository, ClientCardRepository>();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Host=postgres.db;Database=bookmaster_DB;Username=postgres;Password=1234")));

MapsterConfig.RegisterMappings();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookMaster3000 API V1");
});

app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();

        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();

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
