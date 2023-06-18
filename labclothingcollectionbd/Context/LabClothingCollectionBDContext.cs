using labclothingcollectionbd.Models;
using Microsoft.EntityFrameworkCore;


namespace labclothingcollectionbd.Context
{
    public class LabClothingCollectionBDContext : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Pessoas> Pessoas { get; set; }
        public DbSet<Colecoes> Colecoes { get; set; }
        public DbSet<Modelos> Modelos { get; set; }


        public LabClothingCollectionBDContext(DbContextOptions<LabClothingCollectionBDContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuarios>()
                .HasIndex(u => u.IdPessoa)
               .IsUnique();

            modelBuilder.Entity<Pessoas>()
                .HasIndex(u => u.IdPessoa)
                .IsUnique();

            modelBuilder.Entity<Colecoes>()
                .HasIndex(c => c.IdColecaoRelacionada)
                .IsUnique();

            modelBuilder.Entity<Modelos>()
                .HasIndex(m => m.IdModelo)
                .IsUnique();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ServerConnection");
            }
        }
    }
}