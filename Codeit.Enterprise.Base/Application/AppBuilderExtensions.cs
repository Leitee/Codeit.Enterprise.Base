using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIf(this IApplicationBuilder app, bool condition, Func<IApplicationBuilder, IApplicationBuilder> perform)
        {
            return condition ? perform(app) : app;
        }

        public static void UseIf(this IApplicationBuilder app, bool condition, Action<IApplicationBuilder> perform)
        {
            if(condition) perform(app);
        }

        public static IServiceCollection AddIf(this IServiceCollection app, bool condition, Func<IServiceCollection, IServiceCollection> perform)
        {
            return condition ? perform(app) : app;
        }

        public static IServiceCollection AddIf(this IServiceCollection app, IConfiguration conf, bool condition, Func<IServiceCollection, IConfiguration, IServiceCollection> perform)
        {
            return condition ? perform(app, conf) : app;
        }
    }
}
