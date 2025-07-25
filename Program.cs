using System.Text;
using EasyChatBackend.Models;
using EasyChatBackend.Options;
using EasyChatBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.IdentityModel.Tokens;

namespace EasyChatBackend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Headers.TryGetValue("Token", out var token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        builder.Services.AddAuthorization();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddOpenApiDocument();

        builder.Services.AddCaptcha(builder.Configuration);

        builder.Services.AddHybridCache();

        builder.Services.AddDbContext<EasyChatContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(builder.Configuration["ConnectionStrings:AccountConnection"]));

        builder.Services.AddTransient<SeedData>();

        builder.Services.AddTransient<Random>();
        builder.Services.AddScoped<AccountService>();
        builder.Services.AddScoped<GroupService>();

        builder.Services.AddSingleton<SysSetting>(provider =>
        {
            var cache = provider.GetService<HybridCache>()!;

            var setting = cache.GetOrCreateAsync<SysSetting?>(SysSetting.Key,
                _ => ValueTask.FromResult(builder.Configuration.GetSection(SysSetting.Key).Get<SysSetting>())).Result!;

            return setting;
        });

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