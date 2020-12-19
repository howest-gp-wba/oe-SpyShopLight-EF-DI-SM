using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wba.SpyshopActual.Domain.Entitys;

namespace Wba.SpyshopActual.Domain.Repositorys
{
    public class ProductRepository
    {

        public IEnumerable<Product> Products { get; set; }

        public ProductRepository()
        {
            Products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "SpyWatch",
                    Description = "The built-in camera in this watch  secured videofeed to a nearby monitor.",
                    Price = 39.95M,
                    PhotoUrl = "watch.jpg",
                    SortNumber = 1,
                    Category = new Category() { Id = 1, Name = "Cameras" }
                },
                new Product
                {
                    Id = 2,
                    Name = "SpyWatch With Storage",
                    Description = "The built-in camera in this watch cured videofeed saved locally on a microchip.",
                    Price = 39.95M,
                    PhotoUrl = "watch.jpg",
                    SortNumber = 2,
                    Category = new Category() { Id = 1, Name = "Cameras" }
                },
                new Product
                {
                    Id = 3,
                    Name = "Communication Pencil",
                    Description = "A small transmitter shaped as a in the tip and listen at the others.",
                    Price = 6.95M,
                    PhotoUrl = "pencil.jpg",
                    SortNumber = 1,
                    Category = new Category() { Id = 2, Name = "Communication" }

                },
                new Product
                {
                    Id=4,
                    Name= "Point of View Penn",
                    Description = "Point this pen at your discussion partner to convince him of your point of view.Writes in blueink.",
                    Price = 9.95M,
                    PhotoUrl = "pen.jpg",
                    SortNumber=2,
                    Category = new Category(){Id=2, Name = "Communication" }
                },
                new Product
                {
                    Id=5,
                    Name="Cigar LaserPointer",
                    Description="Evil geniuses have cats. Distract them with this laser pointer. Disperses catnip as a last resort.",
                    Price=13.95M,
                    PhotoUrl="cigar.jpg",
                    SortNumber=3,
                    Category = new Category(){Id=2, Name = "Communication" }
                },
                new Product
                {
                    Id=6,
                    Name="Mustach Translator",
                    Description = "Advanced voice-technology ensures this moustasche translates every word you say in the language of choice.",
                    Price=73.95M,
                    PhotoUrl="stash.jpg",
                    SortNumber=4,
                    Category = new Category(){Id=2, Name = "Communication" }
                }
            };
        }
        public Product GetProductById(long productId)
        {
            return Products.FirstOrDefault(p => p.Id == productId);
        }
    }
}