using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using X5on.Authorization.Roles;
using X5on.Authorization.Users;
using X5on.MultiTenancy;
using Microsoft.Extensions.Logging;

namespace X5on.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options, 
            SignInManager signInManager,
            ISystemClock systemClock,
            ILoggerFactory loggerFactory) 
            : base(
                  options, 
                  signInManager, 
                  systemClock,
                  loggerFactory)
        {
        }
    }
}
