using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StringProcessor.Demo
{
    class Startup
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetService<Program>().Run(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                .AddTransient<Program>();
        }
    }
}
