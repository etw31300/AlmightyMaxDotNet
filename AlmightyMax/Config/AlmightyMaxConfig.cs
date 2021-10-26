using System;
using System.IO;
using System.Text;
using AlmightyMax.Domain.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AlmightyMax.Config
{
    public class AlmightyMaxConfig
    {
        public IConfiguration Config { get; private set; }

        public static IConfiguration UseDefaultConfig => new AlmightyMaxConfig().Config;

        public AlmightyMaxConfig()
        {
            // Grab secrets and add them to configuration
            Config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddUserSecrets<AlmightyMaxSecrets>()
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<AlmightyMaxSecrets>(Config.GetSection(nameof(AlmightyMaxSecrets)))
                .AddOptions()
                .BuildServiceProvider();

            serviceProvider.GetService<AlmightyMaxSecrets>();
        }
    }
}
