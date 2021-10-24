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
                .AddEnvironmentVariables()
                .AddUserSecrets<AlmightyMaxSecrets>()
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<AlmightyMaxSecrets>(SecretsConfig.GetSection(nameof(AlmightyMaxSecrets)))
                .AddOptions()
                .BuildServiceProvider();

            serviceProvider.GetService<AlmightyMaxSecrets>();
            
            // Bring in basic application configuration for JSON Deserialization
            var json = "";

            using var fs = File.OpenRead($@"{AppDomain.CurrentDomain.BaseDirectory}\appsettings.json");
            using var sr = new StreamReader(fs, Encoding.UTF8);
            json = sr.ReadToEnd();
            
            
        }
    }
}
