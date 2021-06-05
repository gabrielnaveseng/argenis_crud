using Microsoft.Extensions.Configuration;
using Argenis.CRUD.Shared.Configurations;
using Argenis.CRUD.Shared.Models;

namespace Argenis.CRUD.Extensions
{
    public static class ConfigurationExtensions
    {
        public static ApplicationConfig LoadConfiguration(this IConfiguration source)
        {
            var applicationConfig = source.Get<ApplicationConfig>();

            applicationConfig.CorsOrigins = source.GetSection("CorsOrigins").Get<string[]>();

            return applicationConfig;
        }
    }
}
