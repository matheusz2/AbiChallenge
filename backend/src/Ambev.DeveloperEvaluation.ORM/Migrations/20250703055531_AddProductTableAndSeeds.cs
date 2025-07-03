using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTableAndSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RatingRate = table.Column<double>(type: "float", nullable: false),
                    RatingCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Image", "Price", "RatingCount", "RatingRate", "Title" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440001"), "men's clothing", "Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday", "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg", 109.95m, 120, 3.8999999999999999, "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440002"), "men's clothing", "Slim-fitting style, contrast raglan long sleeve, three-button henley placket, light weight & soft fabric for breathable and comfortable wearing. And Solid stitched shirts with round neck made for durability and a great fit for casual fashion wear and diehard baseball fans. The Henley style round neckline includes a three-button placket.", "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg", 22.3m, 259, 4.0999999999999996, "Mens Casual Premium Slim Fit T-Shirts" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440003"), "men's clothing", "great outerwear jackets for Spring/Autumn/Winter, suitable for many occasions, such as working, hiking, camping, mountain/rock climbing, cycling, traveling or other outdoors. Good gift choice for you or your family member. A warm hearted love to Father, husband or son in this thanksgiving or Christmas Day.", "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg", 55.99m, 500, 4.7000000000000002, "Mens Cotton Jacket" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440004"), "men's clothing", "The color could be slightly different between on the screen and in practice. Please note that body builds vary by person, therefore, detailed size information should be reviewed below on the product description.", "https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_.jpg", 15.99m, 430, 2.1000000000000001, "Mens Casual Slim Fit" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440005"), "jewelery", "From our Legends Collection, the Naga was inspired by the mythical water dragon that protects the ocean's pearl. Wear facing inward to be bestowed with love and abundance, or outward for protection.", "https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_.jpg", 695.00m, 400, 4.5999999999999996, "John Hardy Women's Legends Naga Gold & Silver Dragon Station Chain Bracelet" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440006"), "jewelery", "Satisfaction Guaranteed. Return or exchange any order within 30 days. Designed and sold by Hafeez Center in the United States. Satisfaction Guaranteed. Return or exchange any order within 30 days.", "https://fakestoreapi.com/img/61sbMiUnoGL._AC_UL640_QL65_ML3_.jpg", 168.00m, 70, 3.8999999999999999, "Solid Gold Petite Micropave" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440007"), "jewelery", "Classic Created Wedding Engagement Solitaire Diamond Promise Ring for Her. Gifts to spoil your love more for Engagement, Wedding, Anniversary, Valentine's Day...", "https://fakestoreapi.com/img/71YAIFU48IL._AC_UL640_QL65_ML3_.jpg", 9.99m, 400, 3.0, "White Gold Plated Princess" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440008"), "jewelery", "Rose Gold Plated Double Flared Tunnel Plug Earrings. Made of 316L Stainless Steel", "https://fakestoreapi.com/img/51UDEzMJVpL._AC_UL640_QL65_ML3_.jpg", 10.99m, 100, 1.8999999999999999, "Pierced Owl Rose Gold Plated Stainless Steel Double" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440009"), "electronics", "USB 3.0 and USB 2.0 Compatibility Fast data transfers Improve PC Performance High Capacity; Compatibility Formatted NTFS for Windows 10, Windows 8.1, Windows 7; Reformatting may be required for other operating systems; Compatibility may vary depending on user's hardware configuration and operating system", "https://fakestoreapi.com/img/61IBBVJvSDL._AC_SY879_.jpg", 64.00m, 203, 3.2999999999999998, "WD 2TB Elements Portable External Hard Drive - USB 3.0" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440010"), "electronics", "Easy upgrade for faster boot up, shutdown, application load and response. Boosts burst write performance, making it ideal for typical PC workloads The perfect balance of performance and reliability Read/write speeds of up to 535MB/s/450MB/s", "https://fakestoreapi.com/img/61U7T1koQqL._AC_SX679_.jpg", 109.00m, 470, 2.8999999999999999, "SanDisk SSD PLUS 1TB Internal SSD - SATA III 6 Gb/s" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440011"), "women's clothing", "Note: The Jackets is US standard size, Please choose size as your usual wear Material: 100% Polyester; Detachable Liner Fabric: Warm Fleece. Detachable Functional Liner: Skin Friendly, Lightweigt and Warm.", "https://fakestoreapi.com/img/51Y5NI-I5jL._AC_UX679_.jpg", 56.99m, 235, 2.6000000000000001, "BIYLACLESEN Women's 3-in-1 Snowboard Jacket Winter Coats" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440012"), "women's clothing", "100% POLYURETHANE(shell) 100% POLYESTER(lining) 75% POLYESTER 25% COTTON (SWEATER), Faux leather material for style and comfort", "https://fakestoreapi.com/img/81XH0e8fefL._AC_UY879_.jpg", 29.95m, 340, 2.8999999999999999, "Lock and Love Women's Removable Hooded Faux Leather Moto Biker Jacket" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440013"), "women's clothing", "Lightweight perfect for trip or casual wear. Long sleeve with hooded, adjustable drawstring waist design. Button and zipper front closure raincoat, fully stripes Lined and The Raincoat has 2 side pockets.", "https://fakestoreapi.com/img/71HblAHs5xL._AC_UY879_-2.jpg", 39.99m, 679, 3.7999999999999998, "Rain Jacket Women Windbreaker Striped Climbing Raincoats" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440014"), "women's clothing", "95% RAYON 5% SPANDEX, Made in USA or Imported, Do Not Bleach, Lightweight fabric with great stretch for comfort, Ribbed on sleeves and neckline", "https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_.jpg", 9.85m, 130, 4.7000000000000002, "MBJ Women's Solid Short Sleeve Boat Neck V" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440015"), "women's clothing", "100% Polyester, Machine wash, neck type t-shirt, Machine Wash & Pre Shrunk for a Great Fit, Lightweight, roomy and highly breathable with moisture wicking fabric", "https://fakestoreapi.com/img/51eg55uWmdL._AC_UX679_.jpg", 7.95m, 146, 4.5, "Opna Women's Short Sleeve Moisture" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
