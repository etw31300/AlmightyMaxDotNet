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
        public IConfiguration SecretsConfig { get; private set; }

        public static AlmightyMaxConfig UseDefaultConfig => new AlmightyMaxConfig();

        public AlmightyMaxConfig()
        {
            // Grab secrets and add them to configuration
            SecretsConfig = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddUserSecrets<AlmightyMaxSecrets>()
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<AlmightyMaxSecrets>(SecretsConfig.GetSection(nameof(AlmightyMaxSecrets)))
                .AddOptions()
                .BuildServiceProvider();

            serviceProvider.GetService<AlmightyMaxSecrets>();
        }
    }
}
