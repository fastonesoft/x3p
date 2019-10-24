using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using X5on.Authorization.Roles;
using X5on.Authorization.Users;
using X5on.MultiTenancy;

namespace X5on.EntityFrameworkCore
{
    public class X5onDbContext : AbpZeroDbContext<Tenant, Role, User, X5onDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public X5onDbContext(DbContextOptions<X5onDbContext> options)
            : base(options)
        {
        }
    }
}
