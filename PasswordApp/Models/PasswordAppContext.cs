using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Models
{
    public class PasswordAppContext : DbContext
    {
        public PasswordAppContext() : base("PassAppConection")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasRequired<Category>(b => b.Category)
                .WithMany(a => a.Accounts)
                .HasForeignKey<int>(v => v.IdCategory);
        }
    }
}
