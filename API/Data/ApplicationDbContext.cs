using Microsoft.EntityFrameworkCore;
using ElectronicsStoreAPI.Models;

namespace ElectronicsStoreAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar precisión decimal para SQLite
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Compra>()
                .Property(c => c.PrecioTotal)
                .HasColumnType("decimal(18,2)");

            // Datos de ejemplo
            modelBuilder.Entity<Producto>().HasData(
                new Producto
                {
                    Id = 1,
                    NombreArticulo = "Laptop Dell XPS 15",
                    Marca = "Dell",
                    Precio = 1299.99m,
                    Cantidad = 10,
                    Descripcion = "Laptop de alto rendimiento con procesador Intel Core i7, 16GB RAM, 512GB SSD",
                    FechaRegistro = DateTime.Now
                },
                new Producto
                {
                    Id = 2,
                    NombreArticulo = "iPhone 15 Pro",
                    Marca = "Apple",
                    Precio = 999.99m,
                    Cantidad = 15,
                    Descripcion = "Smartphone de última generación con chip A17 Pro, cámara de 48MP",
                    FechaRegistro = DateTime.Now
                },
                new Producto
                {
                    Id = 3,
                    NombreArticulo = "Samsung Galaxy S24",
                    Marca = "Samsung",
                    Precio = 899.99m,
                    Cantidad = 20,
                    Descripcion = "Smartphone Android con pantalla AMOLED, 256GB almacenamiento",
                    FechaRegistro = DateTime.Now
                }
            );
        }
    }
}
