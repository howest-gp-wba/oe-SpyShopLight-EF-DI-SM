using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Domain.Entitys;

namespace Wba.SpyshopActual.Web.Data
{
    public class SpyShopContext : DbContext
    {
        public SpyShopContext(DbContextOptions<SpyShopContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Hier komt de Seeding data en Fluent API

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("money");



            //modelBuilder.Entity<Category>()
            //    .HasMany(e => e.Products)
            //    .WithOne(p => p.Category)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);



            DataSeeder.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

    }
}
