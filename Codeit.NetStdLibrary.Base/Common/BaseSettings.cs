/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Common
{
    using Microsoft.Extensions.Configuration;

    public class BaseSettings<T> where T : class
    {
        public string Environment { get; set; }
        public bool IsDevelopment { get { return Environment == "Development"; } }

        public static T GetSettings(IConfiguration config)
        {
            return config.Get<T>();
        }

        public static T GetSection(IConfiguration config)
        {
            return config.GetSection(typeof(T).Name).Get<T>();
        }

    }

    public class BaseSettings : BaseSettings<BaseSettings>
    {

    }
}
