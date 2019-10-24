using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace X5on.Controllers
{
    public abstract class X5onControllerBase: AbpController
    {
        protected X5onControllerBase()
        {
            LocalizationSourceName = X5onConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
