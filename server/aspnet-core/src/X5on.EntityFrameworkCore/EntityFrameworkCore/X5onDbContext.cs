using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using X5on.Authorization.Roles;
using X5on.Authorization.Users;
using X5on.MultiTenancy;
using Abp.Localization;

namespace X5on.EntityFrameworkCore
{
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class X5onDbContext : AbpZeroDbContext<Tenant, Role, User, X5onDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public X5onDbContext(DbContextOptions<X5onDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationLanguageText>().Property(p => p.Value).HasMaxLength(500);
            base.OnModelCreating(modelBuilder);
        }
    }
}
