using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace web_back_tictactoe.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .PreferHostingUrls(true)
                .UseUrls("http://localhost:5000")
                .UseStartup<Startup>();
        }
    }
}