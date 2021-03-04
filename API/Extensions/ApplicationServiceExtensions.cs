using System;
using API.Data;
using API.Data.Billiards;
using API.Data.PBA;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.Billiards;
using API.Interfaces.PBA;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            // strongly type Configure<class>(where to get)
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings")); 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGamesRepository, GamesRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IBilliardsTournamentRepository, BilliardsTournamentRepository>();
            services.AddScoped<IBilliardsTournamentMembersRepository, BilliardsTournamentMembersRepository>();
            services.AddScoped<IBilliardsMatchTypesRepository, BilliardsMatchTypesRepository>();
            services.AddScoped<IBilliardsRepository, BilliardsRepository>();
            services.AddScoped<IBilliardsModeRepository, BilliardsModeRepository>();
            services.AddScoped<IBilliardsGameRepository, BilliardsGameRepository>();
            services.AddScoped<ISeasonHistoryRepository, SeasonHistoryRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IUserWins, UserWins>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options => 
            {
                // options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                // options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                // Depending on if in development or production, use either Heroku-provided
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                    connStr = configuration.GetConnectionString("DefaultConnection");
                }
                else
                {
                    // Use connection string provided at runtime by Heroku.
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // Parse connection URL to connection string for Npgsql
                    connUrl = connUrl.Replace("postgres://", string.Empty);
                    var pgUserPass = connUrl.Split("@")[0];
                    var pgHostPortDb = connUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];

                    connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}";
                }

                // Whether the connection string came from the local development configuration file
                // or from the environment variable from Heroku, use it to set up your DbContext.
                options.UseNpgsql(connStr);
            });

            return services;
        }
    }
}