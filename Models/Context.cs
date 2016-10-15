using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace mn_you.Models.SQLite
{
    public class MnyouContext : DbContext
    {
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./mnyou.db");
        }
    }

}