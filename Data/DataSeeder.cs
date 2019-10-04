using FlexeraAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FlexeraAPI.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider app)
        {
            var context = app.GetRequiredService<FlexeraContext>();

            await CreateUsers(context);
        }

        //Adds an intial user to the in memory database
        private static async Task CreateUsers(FlexeraContext context)
        {
            context.Add(new Person
            {
                Name = "Test User",
                Address = "123 Fake Street",
                Age = 20,
                Balance = 4500,
                Email = "testuser@gmail.com"
            });

            await context.SaveChangesAsync();
        }
    }
}
