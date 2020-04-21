using Dna;
using Dna.AspNet;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Main entry of the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry of the application
        /// </summary>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                .Build();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {

            return WebHost.CreateDefaultBuilder(args)
                .UseDnaFramework(x => x.AddFileLogger("log.txt"))
                .UseKestrel(x => x.Listen(IPAddress.Loopback, 7000))
                .UseStartup<Startup>();

        }

    }
}
