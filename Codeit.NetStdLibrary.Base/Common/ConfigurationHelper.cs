using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Codeit.NetStdLibrary.Base.Common
{
    public static partial class CodeitUtils
    {
        //Development is default for support EF design time factory 
        private static string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") 
            ?? EnvironmentName.Development; 

        public static IConfiguration BuildDefaultSettings(IConfigurationBuilder configurationBuilder)
        {
            Console.WriteLine($"Environment: {environmentName}");

            var basePath = Directory.GetCurrentDirectory();
            var configuration = configurationBuilder
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false )
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
