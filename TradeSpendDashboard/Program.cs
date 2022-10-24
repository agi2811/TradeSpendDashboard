using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using SerilogTimings;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("logsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"logsettings.{ENVIRONMENT}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Information($"ENVI TYPE : {ENVIRONMENT}");

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Application", typeof(Program).Assembly.GetName().Name)
                .Enrich.WithProperty("Environment", ENVIRONMENT)
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() })
                ).CreateLogger();

            try
            {
                var seed = args.Contains("/seed");
                var ensureDeleted = args.Contains("/delete");

                if (seed)
                    args = args.Except(new[] { "/seed" }).ToArray();

                if (ensureDeleted)
                    args = args.Except(new[] { "/delete" }).ToArray();

                var host = CreateHostBuilder(args).Build();
                var config = host.Services.GetRequiredService<IConfiguration>();

                Log.Information("Seeding data : " + seed.ToString());

                //if (seed)
                //{
                //    using (var scope = host.Services.CreateScope())
                //    {
                //        var services = scope.ServiceProvider;
                //        var hostingEnvironment = services.GetService<IWebHostEnvironment>();
                //        var Configuration = services.GetService<IConfiguration>();

                //        using (var op = Operation.Begin("Seeding data..."))
                //        {
                //            await SeedData.EnsureSeedData(services, ensureDeleted);

                //            op.Complete();
                //        }
                //    }

                //    return 0;
                //}

                Log.Information("Starting host Web Product Return...");
                host.Run();
                Log.Information("Web Product Return Started");

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "API Master Data terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
