using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleRemember.Database.Table;
using TeleRemember.Server;

namespace TeleRemember.Database
{
    public class ListDbContext : DbContext
    {
        public DbSet<ToDo> ToDo { get; set; }

        public ListDbContext(
            DbContextOptions<ListDbContext> options) 
            : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("List");
            modelBuilder.Entity<ToDo>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<ToDo>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWID()");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
