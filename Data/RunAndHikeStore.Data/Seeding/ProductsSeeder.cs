namespace RunAndHikeStore.Data.Seeding
{
    using RunAndHikeStore.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Products.Any())
            {
                return;
            }

            var initialProducts = new List<Product>()
                                            {
                                              new Product
                                              {
                                                   Id = "3bbf392e-9cd6-47bf-8ed1-1666c2dc6065",
                                                   ProductNumber = "45656582",
                                                   Name = "Speedgoat 5 W Fuschia",
                                                   Description = "The speedgoat 5 has been designed for sprint races and training sessions of more than 60 minutes.The technical mesh design on the upper provides increased breathability and elasticity.Weight: 292g (size 42).Drop: 5 mm ",
                                                   UnitPrice = 149m,
                                                   Color = "Pink",
                                                   ImageUrl = "/images/product-hoka-speedgoat-5-fuschia-women.png",
                                                   Gender = Models.Enums.Gender.Female,
                                                   BrandId = "95a5446c-d8c6-48ba-a58b-e1d81b958eec",
                                                   ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                              },
                                              new Product
                                              {
                                                   Id = "2ae0c740-a032-4413-bec8-77df54439edd",
                                                   ProductNumber = "45656583",
                                                   Name = "Speedgoat 5 W Deep Teal",
                                                   Description = "The speedgoat 5 has been designed for sprint races and training sessions of more than 60 minutes.The technical mesh design on the upper provides increased breathability and elasticity.Weight: 292g (size 42).Drop: 5 mm ",
                                                   UnitPrice = 140m,
                                                   Color = "Light Blue",
                                                   ImageUrl = "/images/product-hoka-speedgoat-5-deep-teal-women.png",
                                                   Gender = Models.Enums.Gender.Female,
                                                   BrandId = "95a5446c-d8c6-48ba-a58b-e1d81b958eec",
                                                   ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                              },
                                              new Product
                                              {
                                                   Id = "3c70d9d3-8a16-4f08-b7de-305007090be4",
                                                   Name = "Speedgoat 5 Amber Yellow",
                                                   ProductNumber = "45656584",
                                                   Description = "The speedgoat 5 has been designed for sprint races and training sessions of more than 60 minutes.The technical mesh design on the upper provides increased breathability and elasticity.Weight: 292g (size 42).Drop: 5 mm ",
                                                   UnitPrice = 135m,
                                                   Color = "Yellow",
                                                   ImageUrl = "/images/product-hoka-speedgoat-5-amber-yellow-men.png",
                                                   Gender = Models.Enums.Gender.Male,
                                                   BrandId = "95a5446c-d8c6-48ba-a58b-e1d81b958eec",
                                                   ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                              },
                                              new Product
                                              {
                                                   Id = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                   ProductNumber = "45656585",
                                                   Name = "Helios III Space",
                                                   Description = "The Helios III Space has been designed for sprint races and training sessions of more than 60 minutes.The technical mesh design on the upper provides increased breathability and elasticity.Weight: 298g (size 42).Drop: 4 mm",
                                                   UnitPrice = 150m,
                                                   Color = "Blue",
                                                   ImageUrl = "/images/product-la-sportiva-helios-3-space.png",
                                                   Gender = Models.Enums.Gender.Male,
                                                   BrandId = "04293eea-02c7-4bc4-a13c-7edd2edb6e28",
                                                   ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                              },
                                              new Product
                                              {
                                                   Id = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                   ProductNumber = "45656586",
                                                   Name = "Peregrine 12 W",
                                                   Description = "The Saucony Peregrine 12 has been designed with Isofit to adapt to your foot on any type of surface.Provide a combination of cushioning, durability and traction never seen before. Weight: 301g (size 42). Drop: 4mm",
                                                   UnitPrice = 139m,
                                                   Color = "Light Blue",
                                                   ImageUrl = "/images/product-saucony-peregrine-12-women.png",
                                                   Gender = Models.Enums.Gender.Female,
                                                   BrandId = "a1b82e61-1900-427e-927c-492dc771e9c0",
                                                   ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                              },
                                              new Product
                                              {
                                                   Id = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                   ProductNumber = "45656587",
                                                   Name = "Karacal",
                                                   Description = "The La Sportiva Karacal has been designed with Isofit to adapt to your foot on any type of surface. Provide a combination of cushioning, durability and traction never seen before. Weight: 290g (size 42). Drop: 7mm",
                                                   UnitPrice = 130m,
                                                   Color = "Light Green",
                                                   ImageUrl = "/images/product-la-sportiva-karacal-men.png",
                                                   Gender = Models.Enums.Gender.Male,
                                                   BrandId = "04293eea-02c7-4bc4-a13c-7edd2edb6e28",
                                                   ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                              },
                                              new Product
                                              {
                                                   Id = "8c30054f-205c-4531-8249-d1490594daab",
                                                   ProductNumber = "45656588",
                                                   Name = "Jacket Arland 3IN1",
                                                   Description = "The Arland 3IN1 M men's jacket consists of an outer jacket and a fleece, which can be worn together in winter, and in the remaining seasons - together or separately depending on the weather conditions. The outer jacket is wind and water resistant, and the fleece provides comfort in cold weather.",
                                                   UnitPrice = 220m,
                                                   Color = "Blue",
                                                   ImageUrl = "/images/product-jack-wolfskin-jacket-arland.jpg",
                                                   Gender = Models.Enums.Gender.Male,
                                                   BrandId = "f3321950-dfd8-4635-83fa-c3a5b9888daf",
                                                   ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                              },
                                              new Product
                                              {
                                                   Id = "3f671721-f5e4-4c98-84ef-199f389707fc",
                                                   ProductNumber = "45656589",
                                                   Name = "Jacket Eagle peak II Softshell",
                                                   Description = "Women's Softshell jacket that protects you from wind and light rain. It is made of water-repellent, windproof, breathable and elastic fabric for freedom of movement.",
                                                   UnitPrice = 190m,
                                                   Color = "Pink",
                                                   ImageUrl = "/images/product-jack-wolfskin-eagle-peak.jpg",
                                                   Gender = Models.Enums.Gender.Female,
                                                   BrandId = "f3321950-dfd8-4635-83fa-c3a5b9888daf",
                                                   ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                              },
                                              new Product
                                              {
                                                   Id = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                                   ProductNumber = "45656511",
                                                   Name = "T-Shirt Easy",
                                                   Description = "Soft and comfortable women's t-shirt made of cotton fabric for comfort.",
                                                   UnitPrice = 35m,
                                                   Color = "White",
                                                   ImageUrl = "/images/product-north-face-t-shirt.jpg",
                                                   Gender = Models.Enums.Gender.Female,
                                                   BrandId = "f94d2830-7038-4c14-a724-701db2179091",
                                                   ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                              },
                                              new Product
                                              {
                                                   Id = "f6e4d33b-8704-44f8-b83f-6992563c222c",
                                                   ProductNumber = "45656512",
                                                   Name = "T-Shirt Easy",
                                                   Description = "Soft and comfortable men's t-shirt made of cotton fabric for comfort.",
                                                   UnitPrice = 35m,
                                                   Color = "Grey",
                                                   ImageUrl = "/images/product-north-face-t-shirt-man.jpg",
                                                   Gender = Models.Enums.Gender.Male,
                                                   BrandId = "f94d2830-7038-4c14-a724-701db2179091",
                                                   ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                              },
                                              new Product
                                              {
                                                   Id = "99214889-61aa-4285-a80b-1ae5281f11b0",
                                                   ProductNumber = "45656515",
                                                   Name = "Instinct",
                                                   Description = "The Garmin Instinct is a rugged outdoor sports watch that comes from the same wearable stable as Garmin's Fenix range.Built to withstand the toughest of elements, the Instinct has all the usual skills you'd expect to see from one of Garmin's outdoor-friendly watches.There's GPS along with GLONASS and Galileo support to offer plenty of mapping coverage. There's a barometric altimeter to measure elevation when you're climbing up mountains, a heart rate monitor and a promise of battery life that will go the distance.",
                                                   UnitPrice = 249.99m,
                                                   Color = "Black",
                                                   ImageUrl = "/images/product-garmin-instinct.jpg",
                                                   Gender = Models.Enums.Gender.Unisex,
                                                   BrandId = "761fe0f9-dda4-48a8-bcaa-0dd04e0bc2d7",
                                                   ProductTypeId = "d2cefad3-9f34-4256-bfbd-23a875436450",
                                              },
                                              new Product
                                              {
                                                   Id = "99214889-61aa-4285-a80b-1ae5281f11ba",
                                                   ProductNumber = "45656517",
                                                   Name = "Lava",
                                                   Description = "The Coros Lava is a rugged outdoor sports watch.Built to withstand the toughest of elements, the watch has all the usual skills you'd expect to see from one of Coros's outdoor-friendly watches.There's GPS along with GLONASS and Galileo support to offer plenty of mapping coverage. There's a barometric altimeter to measure elevation when you're climbing up mountains, a heart rate monitor and a promise of battery life that will go the distance.",
                                                   UnitPrice = 599.99m,
                                                   Color = "Orange",
                                                   ImageUrl = "/images/product-coros-vertix-2-lava.png",
                                                   Gender = Models.Enums.Gender.Male,
                                                   BrandId = "f1b41645-1b83-4975-a5d1-26c713c25321",
                                                   ProductTypeId = "d2cefad3-9f34-4256-bfbd-23a875436450",
                                              },
                                              new Product
                                              {
                                                   Id = "99214889-61aa-4285-a80b-1ae5281f11bb",
                                                   ProductNumber = "45656519",
                                                   Name = "Pace 2",
                                                   Description = "The Coros Pace 2 is a budget outdoor sports watch. The watch has all the usual skills you'd expect to see from one of Coros's outdoor watches.There's GPS along with GLONASS and Galileo support to offer plenty of mapping coverage. There's a barometric altimeter to measure elevation when you're climbing up mountains, a heart rate monitor and a promise of battery life that will go the distance.",
                                                   UnitPrice = 189.99m,
                                                   Color = "Orange",
                                                   ImageUrl = "/images/product-coros-pace-2-red.png",
                                                   Gender = Models.Enums.Gender.Unisex,
                                                   BrandId = "f1b41645-1b83-4975-a5d1-26c713c25321",
                                                   ProductTypeId = "d2cefad3-9f34-4256-bfbd-23a875436450",
                                              },
                                            };

            await dbContext.Products.AddRangeAsync(initialProducts);
        }
    }
}