using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace X5on.Configuration
{
    public static class HostEnvironmentEnvExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IHostEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}
