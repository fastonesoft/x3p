using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using X5on.Configuration;

namespace X5on.Web.Host.Startup
{
    [DependsOn(
       typeof(X5onWebCoreModule))]
    public class X5onWebHostModule: AbpModule
    {
        private readonly IHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public X5onWebHostModule(IHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(X5onWebHostModule).GetAssembly());
        }
    }
}
