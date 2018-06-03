using System;
using System.IO;
using System.Threading.Tasks;
using StructureMap;
using Microsoft.Extensions.Configuration;

namespace AglDevelopersTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootContainer = ConfigureIoc();

            var configuration = InitaliseConfiguration();
            rootContainer.Inject<IConfiguration>(configuration);

            using (var nestedCont = rootContainer.GetNestedContainer())
            {
                var dataReport = nestedCont.GetInstance<IDataReport>();
                await dataReport.RunReportAsync();
            }
        }

        private static IConfigurationRoot InitaliseConfiguration() => 
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

        private static IContainer ConfigureIoc() => 
            new Container(x =>
            {
                x.Scan(s =>
                {
                    s.AssembliesFromApplicationBaseDirectory();
                    s.LookForRegistries();

                });
            });
    }
}
