using EasyChatBackend.Models;
using EasyChatBackend.Options;
using EasyChatBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddOpenApiDocument();

        builder.Services.AddCaptcha(builder.Configuration);

        builder.Services.AddHybridCache();

        builder.Services.AddDbContext<AccountContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(builder.Configuration["ConnectionStrings:AccountConnection"]));

        builder.Services.AddTransient<SeedData>();


        builder.Services.AddTransient<Random>();
        builder.Services.AddScoped<AccountService>();

        builder.Services.Configure<AccountOptions>(
            builder.Configuration.GetSection(AccountOptions.Position));

        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Position));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.MapOpenApi();
            app.UseSwaggerUi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();


        bool cmdLineInit = (builder.Configuration["INITDB"] ?? "false") == "true";
        if (builder.Environment.IsDevelopment() || cmdLineInit)
        {
            using var scope = app.Services.CreateScope();
            var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();
            seedData.SeedDatabase();
            if (cmdLineInit)
            {
                var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
                lifetime.StopApplication();
                return;
            }
        }

        app.Run();
    }
}