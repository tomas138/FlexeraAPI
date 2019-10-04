using FlexeraAPI.Data;
using FlexeraAPI.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlexeraAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            //using (var serviceScope = webHost.Services.CreateScope())
            //{
            //   serviceScope.ServiceProvider.GetRequiredService<FlexeraContext>().Database.Migrate();
            //}

            using (var scope = webHost.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<FlexeraContext>())
            {               
                context.Database.EnsureCreated();
                DataSeeder.SeedAsync(scope.ServiceProvider).Wait();
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
