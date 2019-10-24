using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using X5on.Authorization;

namespace X5on
{
    [DependsOn(
        typeof(X5onCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class X5onApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<X5onAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(X5onApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
