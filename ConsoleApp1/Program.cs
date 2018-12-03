using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up program to read appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration Configuration = builder.Build();

            // Use the Bind() method to read the particular section in Configuration to a DemoStuffSettings object
            var demo = new DemoStuffSettings();
            Configuration.GetSection("DemoStuff").Bind(demo);

            Console.WriteLine(demo.Setting1);
            Console.WriteLine(demo.Setting2);


            // Now if you want to add this object type to DI...
            IServiceCollection services = new ServiceCollection();
            services.Configure<DemoStuffSettings>(Configuration.GetSection("DemoStuff"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<DemoStuffSettings>>().Value);

            var serviceProvider = services.BuildServiceProvider();

            var test = serviceProvider.GetService<DemoStuffSettings>();

            Console.WriteLine(test.Setting1);
            Console.WriteLine(test.Setting2);
        }

    }
}
