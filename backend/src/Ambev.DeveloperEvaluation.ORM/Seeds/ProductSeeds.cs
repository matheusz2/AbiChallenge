using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ambev.DeveloperEvaluation.ORM.Seeds;

public static class ProductSeeds
{
    public static void SeedProducts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
                Title = "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
                Price = 109.95m,
                Description = "Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday",
                Category = "men's clothing",
                Image = "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
                RatingRate = 3.9,
                RatingCount = 120
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
                Title = "Mens Casual Premium Slim Fit T-Shirts",
                Price = 22.3m,
                Description = "Slim-fitting style, contrast raglan long sleeve, three-button henley placket, light weight & soft fabric for breathable and comfortable wearing. And Solid stitched shirts with round neck made for durability and a great fit for casual fashion wear and diehard baseball fans. The Henley style round neckline includes a three-button placket.",
                Category = "men's clothing",
                Image = "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg",
                RatingRate = 4.1,
                RatingCount = 259
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440003"),
                Title = "Mens Cotton Jacket",
                Price = 55.99m,
                Description = "great outerwear jackets for Spring/Autumn/Winter, suitable for many occasions, such as working, hiking, camping, mountain/rock climbing, cycling, traveling or other outdoors. Good gift choice for you or your family member. A warm hearted love to Father, husband or son in this thanksgiving or Christmas Day.",
                Category = "men's clothing",
                Image = "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg",
                RatingRate = 4.7,
                RatingCount = 500
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440004"),
                Title = "Mens Casual Slim Fit",
                Price = 15.99m,
                Description = "The color could be slightly different between on the screen and in practice. Please note that body builds vary by person, therefore, detailed size information should be reviewed below on the product description.",
                Category = "men's clothing",
                Image = "https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_.jpg",
                RatingRate = 2.1,
                RatingCount = 430
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440005"),
                Title = "John Hardy Women's Legends Naga Gold & Silver Dragon Station Chain Bracelet",
                Price = 695.00m,
                Description = "From our Legends Collection, the Naga was inspired by the mythical water dragon that protects the ocean's pearl. Wear facing inward to be bestowed with love and abundance, or outward for protection.",
                Category = "jewelery",
                Image = "https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_.jpg",
                RatingRate = 4.6,
                RatingCount = 400
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440006"),
                Title = "Solid Gold Petite Micropave",
                Price = 168.00m,
                Description = "Satisfaction Guaranteed. Return or exchange any order within 30 days. Designed and sold by Hafeez Center in the United States. Satisfaction Guaranteed. Return or exchange any order within 30 days.",
                Category = "jewelery",
                Image = "https://fakestoreapi.com/img/61sbMiUnoGL._AC_UL640_QL65_ML3_.jpg",
                RatingRate = 3.9,
                RatingCount = 70
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440007"),
                Title = "White Gold Plated Princess",
                Price = 9.99m,
                Description = "Classic Created Wedding Engagement Solitaire Diamond Promise Ring for Her. Gifts to spoil your love more for Engagement, Wedding, Anniversary, Valentine's Day...",
                Category = "jewelery",
                Image = "https://fakestoreapi.com/img/71YAIFU48IL._AC_UL640_QL65_ML3_.jpg",
                RatingRate = 3.0,
                RatingCount = 400
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440008"),
                Title = "Pierced Owl Rose Gold Plated Stainless Steel Double",
                Price = 10.99m,
                Description = "Rose Gold Plated Double Flared Tunnel Plug Earrings. Made of 316L Stainless Steel",
                Category = "jewelery",
                Image = "https://fakestoreapi.com/img/51UDEzMJVpL._AC_UL640_QL65_ML3_.jpg",
                RatingRate = 1.9,
                RatingCount = 100
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440009"),
                Title = "WD 2TB Elements Portable External Hard Drive - USB 3.0",
                Price = 64.00m,
                Description = "USB 3.0 and USB 2.0 Compatibility Fast data transfers Improve PC Performance High Capacity; Compatibility Formatted NTFS for Windows 10, Windows 8.1, Windows 7; Reformatting may be required for other operating systems; Compatibility may vary depending on user's hardware configuration and operating system",
                Category = "electronics",
                Image = "https://fakestoreapi.com/img/61IBBVJvSDL._AC_SY879_.jpg",
                RatingRate = 3.3,
                RatingCount = 203
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440010"),
                Title = "SanDisk SSD PLUS 1TB Internal SSD - SATA III 6 Gb/s",
                Price = 109.00m,
                Description = "Easy upgrade for faster boot up, shutdown, application load and response. Boosts burst write performance, making it ideal for typical PC workloads The perfect balance of performance and reliability Read/write speeds of up to 535MB/s/450MB/s",
                Category = "electronics",
                Image = "https://fakestoreapi.com/img/61U7T1koQqL._AC_SX679_.jpg",
                RatingRate = 2.9,
                RatingCount = 470
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440011"),
                Title = "BIYLACLESEN Women's 3-in-1 Snowboard Jacket Winter Coats",
                Price = 56.99m,
                Description = "Note: The Jackets is US standard size, Please choose size as your usual wear Material: 100% Polyester; Detachable Liner Fabric: Warm Fleece. Detachable Functional Liner: Skin Friendly, Lightweigt and Warm.",
                Category = "women's clothing",
                Image = "https://fakestoreapi.com/img/51Y5NI-I5jL._AC_UX679_.jpg",
                RatingRate = 2.6,
                RatingCount = 235
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440012"),
                Title = "Lock and Love Women's Removable Hooded Faux Leather Moto Biker Jacket",
                Price = 29.95m,
                Description = "100% POLYURETHANE(shell) 100% POLYESTER(lining) 75% POLYESTER 25% COTTON (SWEATER), Faux leather material for style and comfort",
                Category = "women's clothing",
                Image = "https://fakestoreapi.com/img/81XH0e8fefL._AC_UY879_.jpg",
                RatingRate = 2.9,
                RatingCount = 340
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440013"),
                Title = "Rain Jacket Women Windbreaker Striped Climbing Raincoats",
                Price = 39.99m,
                Description = "Lightweight perfect for trip or casual wear. Long sleeve with hooded, adjustable drawstring waist design. Button and zipper front closure raincoat, fully stripes Lined and The Raincoat has 2 side pockets.",
                Category = "women's clothing",
                Image = "https://fakestoreapi.com/img/71HblAHs5xL._AC_UY879_-2.jpg",
                RatingRate = 3.8,
                RatingCount = 679
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440014"),
                Title = "MBJ Women's Solid Short Sleeve Boat Neck V",
                Price = 9.85m,
                Description = "95% RAYON 5% SPANDEX, Made in USA or Imported, Do Not Bleach, Lightweight fabric with great stretch for comfort, Ribbed on sleeves and neckline",
                Category = "women's clothing",
                Image = "https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_.jpg",
                RatingRate = 4.7,
                RatingCount = 130
            },
            new Product
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440015"),
                Title = "Opna Women's Short Sleeve Moisture",
                Price = 7.95m,
                Description = "100% Polyester, Machine wash, neck type t-shirt, Machine Wash & Pre Shrunk for a Great Fit, Lightweight, roomy and highly breathable with moisture wicking fabric",
                Category = "women's clothing",
                Image = "https://fakestoreapi.com/img/51eg55uWmdL._AC_UX679_.jpg",
                RatingRate = 4.5,
                RatingCount = 146
            }
        );
    }
} 