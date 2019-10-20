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
    }
}
