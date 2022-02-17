/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Common
{
    using Microsoft.Extensions.Configuration;
    using System.Linq;

    public class BaseSettings<TSettings> where TSettings : class
    {
        public string Environment { get; set; }
        public bool IsDevelopment { get { return Environment is not "Production"; } }

        public static TSettings GetSettings(IConfiguration config)
        {
            return config.Get<TSettings>();
        }

        public static TSection GetSection<TSection>(IConfiguration config)
        {
            var prueba = config.GetChildren().SingleOrDefault(ch => ch.GetType() == typeof(TSection));

            return config.GetSection(typeof(TSection).Name).Get<TSection>();
        }

    }

    public class BaseSettings : BaseSettings<BaseSettings>
    {

    }
}
