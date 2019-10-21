using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using X5on.Data.Models;

namespace X5on.Data
{
    public class X5onContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public X5onContext(DbContextOptions<X5onContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>();
        }
    }
}
