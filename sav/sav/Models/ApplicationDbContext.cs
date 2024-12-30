using Microsoft.EntityFrameworkCore;
using sav.Models;

namespace sav.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Claim> Claim { get; set; }
        public DbSet<TechnicalIntervention> TechnicalIntervention { get; set; }
        public DbSet<SparePart> SparePart { get; set; }

        public DbSet<Article> Article { get; set; } = null!;



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SparePart>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<TechnicalIntervention>()
                .Property(t => t.LaborCost)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<TechnicalIntervention>()
                .Property(t => t.TotalCost)
                .HasColumnType("decimal(18, 2)");
        }

    }
}
