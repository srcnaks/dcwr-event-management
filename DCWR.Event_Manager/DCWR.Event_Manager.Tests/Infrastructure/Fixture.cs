using System;
using System.Net.Http;
using DCWR.Event_Manager.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace DCWR.Event_Manager.Tests.Infrastructure
{
    public class Fixture : IDisposable
    {
        public const string Url = "http://0.0.0.0:5000";
        public IServiceProvider ServiceProvider { get; }
        public HttpClient Client { get; }

        public Fixture()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>();

            var server = new TestServer(host) { BaseAddress = new Uri(Url) };
            Client = server.CreateClient();
            ServiceProvider = server.Host.Services;
        }

        public void Dispose()
        {
        }

        public IServiceScope CreateScope()
        {
            var scope = ServiceProvider.GetService<IServiceScopeFactory>().CreateScope();
            return scope;
        }
    }
}
