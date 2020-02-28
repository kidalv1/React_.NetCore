using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Migrations;
using Microsoft.AspNetCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();
           using(var score = host.Services.CreateScope())
           {
              var services = score.ServiceProvider;
              try
              {
                var context = services.GetRequiredService<DataContext>();
                context.Database.Migrate();
                Seed.SeedData(context);
                
              }
              catch(Exception ex)
              {
                var loggger = services.GetRequiredService<ILogger>();
                loggger.LogError(ex.Message , " An error during migration" );
              }
           }
          host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
