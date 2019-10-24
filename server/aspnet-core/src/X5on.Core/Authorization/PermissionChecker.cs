using Abp.Authorization;
using X5on.Authorization.Roles;
using X5on.Authorization.Users;

namespace X5on.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
