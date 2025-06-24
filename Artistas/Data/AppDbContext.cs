using Microsoft.EntityFrameworkCore;
using Artistas.Models;

namespace Artistas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artista>().ToTable("Artistas");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");

            modelBuilder.Entity<Categoria>()
                .HasMany(categoria => categoria.Artistas)
                .WithOne(artista => artista.Categoria)
                .HasForeignKey(artista => artista.CategoriaId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Artista>()
                .HasOne(artista => artista.Categoria)
                .WithMany(categoria => categoria.Artistas)
                .HasForeignKey(artista => artista.CategoriaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artista>()
                .HasIndex(artista => artista.Nombre)
                .IsUnique();

            modelBuilder.Entity<Categoria>()
                .HasIndex(categoria => categoria.Nombre)
                .IsUnique();
       }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Artista> Artistas { get; set; }
    }
}
