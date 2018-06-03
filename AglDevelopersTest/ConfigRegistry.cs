using System;
using StructureMap;

namespace AglDevelopersTest
{
    public class ConfigRegistry : Registry
    {
        public ConfigRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });

            For<IDataProvider>().Use<WebDataProvider>().Singleton();    // Only require 1 instance of provider
        }
    }
}
