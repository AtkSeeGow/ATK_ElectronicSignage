using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System.Net;

namespace ElectronicSignage.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options => {
                        options.Listen(IPAddress.Any, 8925);
                        options.Listen(IPAddress.Any, 8926, listenOptions =>
                        {
                            listenOptions.UseHttps(@"/home/ubuntu/certificate.pfx", "000000");
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
