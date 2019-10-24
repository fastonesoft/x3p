using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace X5on.EntityFrameworkCore
{
    public static class X5onDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<X5onDbContext> builder, string connectionString)
        {
            builder.UseMySQL(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<X5onDbContext> builder, DbConnection connection)
        {
            builder.UseMySQL(connection);
        }
    }
}
