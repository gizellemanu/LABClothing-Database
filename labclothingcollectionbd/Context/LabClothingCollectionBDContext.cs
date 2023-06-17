using labclothingcollection.Controllers;
using labclothingcollectionbd.Controllers;
using labclothingcollectionbd.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
                .HasIndex(u => u.Email)
               .IsUnique();

            modelBuilder.Entity<Pessoas>()
                .HasIndex(u => u.CpfCnpj)
                .IsUnique();

            modelBuilder.Entity<Colecoes>()
                .HasIndex(c => c.NomeColecao)
                .IsUnique();

            modelBuilder.Entity<Modelos>()
                .HasIndex(m => m.NomeModelo)
                .IsUnique();
        }
        public List<UsuariosController> Usuario()
        {
          return Set<UsuariosController>().ToList();
        }
        public List<PessoasController> Pessoa()
        {
          return Set<PessoasController>().ToList();
       }
        public List<ColecoesController> Colecao()
        {
          return Set<ColecoesController>().ToList();
        }
        public List<ModelosController> Modelo()
        {
          return Set<ModelosController>().ToList();
        }
    }
}