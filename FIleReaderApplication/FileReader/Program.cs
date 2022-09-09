using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FileReader
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) => {
                    new Startup(context, services).ConfigureServices(services);
                });

        public static void Main(string[] args)
        {
            var applicationHost = CreateHostBuilder(args).Build();
            using (IServiceScope serviceScope = applicationHost.Services.CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<ScanFiles>().Run(args);
            }
            applicationHost.Dispose();
        }
    }
}
