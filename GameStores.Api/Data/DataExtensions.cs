using System;
using GameStores.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStores.Api.Data;

public static class DataExtensions
{
    // Fungsi untuk migrasi DB
    public static void migrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        // Ambil connection string
        var connString = builder.Configuration.GetConnectionString("GameStore");

        builder.Services.AddSqlite<GameStoreContext>(
            connString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Account>().Any())
                {
                    // --- Buat Account ---
                    var passwordHasher = new PasswordHasher<Account>();

                    var user1 = new Account
                    {
                        Name = "User1",
                        Email = "user1@example.test",
                        Role = Role.User
                    };
                    user1.Password = passwordHasher.HashPassword(user1, "password");

                    var developer = new Account
                    {
                        Name = "Rockstar",
                        Email = "rockstar@example.test",
                        Role = Role.Developer
                    };
                    developer.Password = passwordHasher.HashPassword(developer, "password");

                    var admin = new Account
                    {
                        Name = "Admin",
                        Email = "admin@example.test",
                        Role = Role.Admin
                    };
                    admin.Password = passwordHasher.HashPassword(admin, "password");

                    // Simpan dulu semua Account
                    context.Set<Account>().AddRange(user1, developer, admin);
                    context.SaveChanges(); 

                    // --- Buat Developer profile ---
                    if (!context.Set<Developer>().Any())
                    {
                        var developerProfile = new Developer
                        {
                            StudioName = "Rockstar Company",
                            Description = "Rockstar adalah studio pengembang game yang populer dan salah satu yang terbesar",
                            Website = "rockstar.com",
                            AccountId = developer.Id 
                        };
                        context.Set<Developer>().Add(developerProfile);
                        context.SaveChanges();
                    }
                }
            })
        );
    }
}
