using Microsoft.EntityFrameworkCore;
using Euroskills2018.Models;

namespace Euroskills2018.Data
{
    public class EuroskillsContext : DbContext
    {
        public EuroskillsContext(DbContextOptions<EuroskillsContext> options) : base(options)
        {
        }

        public DbSet<Szakma> Szakmak { get; set; }
        public DbSet<Orszag> Orszagok { get; set; }
        public DbSet<Versenyzo> Versenyzok { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Szakma>().HasKey(s => s.Id);
            modelBuilder.Entity<Orszag>().HasKey(o => o.Id);
            modelBuilder.Entity<Versenyzo>().HasKey(v => v.Id);

            modelBuilder.Entity<Versenyzo>()
                .HasOne(v => v.Szakma)
                .WithMany(s => s.Versenyzok)
                .HasForeignKey(v => v.SzakmaId);

            modelBuilder.Entity<Versenyzo>()
                .HasOne(v => v.Orszag)
                .WithMany(o => o.Versenyzok)
                .HasForeignKey(v => v.OrszagId);
        }
    }
}
