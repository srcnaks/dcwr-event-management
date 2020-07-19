using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using DCWR.Event_Manager.WebApp.React;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DCWR.Event_Manager.Tests.Infrastructure
{
    public class Fixture : IDisposable
    {
        public const string Url = "http://0.0.0.0:5000";
        public IServiceProvider ServiceProvider { get; }
        public HttpClient Client { get; }
        private DbContextOptions<EventManagerDbContext> dbContextOptions { get; set; }
        private readonly string dbName;

        public Fixture()
        {
            dbName = CreateDatabase();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(builder => 
                    builder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddInMemoryCollection(CustomSettings())
                );

            var server = new TestServer(host) { BaseAddress = new Uri(Url) };
            Client = server.CreateClient();
            ServiceProvider = server.Host.Services;
        }

        public void Dispose()
        {
            using (var db = GetContext())
            {
                db.Database.EnsureDeleted();
            }
        }

        private Dictionary<string, string> CustomSettings()
        {
            return new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnection", $"Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres;Database={dbName};" },
            };
        }

        private string CreateDatabase()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetSection("ConnectionStrings:DefaultConnection").Get<string>();
            var dbName = $"dcwr-event-manager-{DateTime.Now.Ticks}";
            connectionString = connectionString.Replace("dcwr-event-manager", dbName);

            dbContextOptions = new DbContextOptionsBuilder<EventManagerDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            using (var db = GetContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            }

            // Postgres bug causes that extension types like 'citext' are not reloaded when database is created
            // See: https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL/issues/292
            // The workaround is reload types once manually
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                conn.ReloadTypes();
                conn.Close();
            }

            return dbName;
        }

        public DbContext GetContext() => new EventManagerDbContext(dbContextOptions);
    }
}
