using Persistence.interfaces;
using Persistence.Repositories;
using Application.interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();

        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<LibraryContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Host=postgres.db;Database=bookmaster_DB;Username=postgres;Password=1234")));


        var app = builder.Build();

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