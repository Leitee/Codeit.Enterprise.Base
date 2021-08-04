using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Codeit.NetStdLibrary.Base.Common
{
    public static partial class CodeitUtils
    {
        private static string environmentName = EnvironmentName.Development;
        public static IConfiguration BuildDefaultSettings(IConfigurationBuilder configurationBuilder)
        {
            var basePath = Directory.GetCurrentDirectory();
            var configuration = configurationBuilder
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }
    }

    public static class ConfigurationExtension
    {
        public static IConfiguration BuildDefaultSettings(this IConfigurationBuilder configurationBuilder)
        {
            return CodeitUtils.BuildDefaultSettings(configurationBuilder);
        }
    }
}
