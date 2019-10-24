using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using X5on.Configuration;
using X5on.Web;

namespace X5on.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class X5onDbContextFactory : IDesignTimeDbContextFactory<X5onDbContext>
    {
        public X5onDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<X5onDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            X5onDbContextConfigurer.Configure(builder, configuration.GetConnectionString(X5onConsts.ConnectionStringName));

            return new X5onDbContext(builder.Options);
        }
    }
}
