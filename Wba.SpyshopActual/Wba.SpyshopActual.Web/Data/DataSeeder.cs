using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.SpyshopActual.Domain.Entitys;

namespace Wba.SpyshopActual.Web.Data
{
    public class DataSeeder 
    {

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                
                new Category {Id=1, Name="Cameras" },
                new Category {Id=2, Name="Communication"}
                
                
                );

            modelBuilder.Entity<Product>().HasData(
                
                new Product {Id=1, Name="Spywatch", Description="SpyWatch", Price=39.95M, PhotoUrl="watch.jpg", CategoryId=1 },
                new Product{Id=2, Name="Comminication Pencil", Description="Pencil", Price=9.95M, CategoryId=2, PhotoUrl="pencil.jpg" },
                new Product { Id = 3, Name = "Mustasch Translator", Description = "Mustache",PhotoUrl="stash.jpg",  Price = 9.95M, CategoryId = 2 },
                new Product { Id = 4, Name = "Batteries", Description = "Batteries", Price = 10.95M, CategoryId = 2, PhotoUrl="batteries.jpg" }



                );

        }
    }
}
