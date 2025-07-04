﻿// <auto-generated />
using System;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    [DbContext(typeof(DefaultContext))]
    partial class DefaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Carts", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.CartProduct", b =>
                {
                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("CartId", "ProductId");

                    b.ToTable("CartProducts", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RatingCount")
                        .HasColumnType("integer");

                    b.Property<double>("RatingRate")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440001"),
                            Category = "men's clothing",
                            Description = "Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday",
                            Image = "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
                            Price = 109.95m,
                            RatingCount = 120,
                            RatingRate = 3.8999999999999999,
                            Title = "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440002"),
                            Category = "men's clothing",
                            Description = "Slim-fitting style, contrast raglan long sleeve, three-button henley placket, light weight & soft fabric for breathable and comfortable wearing. And Solid stitched shirts with round neck made for durability and a great fit for casual fashion wear and diehard baseball fans. The Henley style round neckline includes a three-button placket.",
                            Image = "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg",
                            Price = 22.3m,
                            RatingCount = 259,
                            RatingRate = 4.0999999999999996,
                            Title = "Mens Casual Premium Slim Fit T-Shirts"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440003"),
                            Category = "men's clothing",
                            Description = "great outerwear jackets for Spring/Autumn/Winter, suitable for many occasions, such as working, hiking, camping, mountain/rock climbing, cycling, traveling or other outdoors. Good gift choice for you or your family member. A warm hearted love to Father, husband or son in this thanksgiving or Christmas Day.",
                            Image = "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg",
                            Price = 55.99m,
                            RatingCount = 500,
                            RatingRate = 4.7000000000000002,
                            Title = "Mens Cotton Jacket"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440004"),
                            Category = "men's clothing",
                            Description = "The color could be slightly different between on the screen and in practice. Please note that body builds vary by person, therefore, detailed size information should be reviewed below on the product description.",
                            Image = "https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_.jpg",
                            Price = 15.99m,
                            RatingCount = 430,
                            RatingRate = 2.1000000000000001,
                            Title = "Mens Casual Slim Fit"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440005"),
                            Category = "jewelery",
                            Description = "From our Legends Collection, the Naga was inspired by the mythical water dragon that protects the ocean's pearl. Wear facing inward to be bestowed with love and abundance, or outward for protection.",
                            Image = "https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_.jpg",
                            Price = 695.00m,
                            RatingCount = 400,
                            RatingRate = 4.5999999999999996,
                            Title = "John Hardy Women's Legends Naga Gold & Silver Dragon Station Chain Bracelet"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440006"),
                            Category = "jewelery",
                            Description = "Satisfaction Guaranteed. Return or exchange any order within 30 days. Designed and sold by Hafeez Center in the United States. Satisfaction Guaranteed. Return or exchange any order within 30 days.",
                            Image = "https://fakestoreapi.com/img/61sbMiUnoGL._AC_UL640_QL65_ML3_.jpg",
                            Price = 168.00m,
                            RatingCount = 70,
                            RatingRate = 3.8999999999999999,
                            Title = "Solid Gold Petite Micropave"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440007"),
                            Category = "jewelery",
                            Description = "Classic Created Wedding Engagement Solitaire Diamond Promise Ring for Her. Gifts to spoil your love more for Engagement, Wedding, Anniversary, Valentine's Day...",
                            Image = "https://fakestoreapi.com/img/71YAIFU48IL._AC_UL640_QL65_ML3_.jpg",
                            Price = 9.99m,
                            RatingCount = 400,
                            RatingRate = 3.0,
                            Title = "White Gold Plated Princess"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440008"),
                            Category = "jewelery",
                            Description = "Rose Gold Plated Double Flared Tunnel Plug Earrings. Made of 316L Stainless Steel",
                            Image = "https://fakestoreapi.com/img/51UDEzMJVpL._AC_UL640_QL65_ML3_.jpg",
                            Price = 10.99m,
                            RatingCount = 100,
                            RatingRate = 1.8999999999999999,
                            Title = "Pierced Owl Rose Gold Plated Stainless Steel Double"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440009"),
                            Category = "electronics",
                            Description = "USB 3.0 and USB 2.0 Compatibility Fast data transfers Improve PC Performance High Capacity; Compatibility Formatted NTFS for Windows 10, Windows 8.1, Windows 7; Reformatting may be required for other operating systems; Compatibility may vary depending on user's hardware configuration and operating system",
                            Image = "https://fakestoreapi.com/img/61IBBVJvSDL._AC_SY879_.jpg",
                            Price = 64.00m,
                            RatingCount = 203,
                            RatingRate = 3.2999999999999998,
                            Title = "WD 2TB Elements Portable External Hard Drive - USB 3.0"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440010"),
                            Category = "electronics",
                            Description = "Easy upgrade for faster boot up, shutdown, application load and response. Boosts burst write performance, making it ideal for typical PC workloads The perfect balance of performance and reliability Read/write speeds of up to 535MB/s/450MB/s",
                            Image = "https://fakestoreapi.com/img/61U7T1koQqL._AC_SX679_.jpg",
                            Price = 109.00m,
                            RatingCount = 470,
                            RatingRate = 2.8999999999999999,
                            Title = "SanDisk SSD PLUS 1TB Internal SSD - SATA III 6 Gb/s"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440011"),
                            Category = "women's clothing",
                            Description = "Note: The Jackets is US standard size, Please choose size as your usual wear Material: 100% Polyester; Detachable Liner Fabric: Warm Fleece. Detachable Functional Liner: Skin Friendly, Lightweigt and Warm.",
                            Image = "https://fakestoreapi.com/img/51Y5NI-I5jL._AC_UX679_.jpg",
                            Price = 56.99m,
                            RatingCount = 235,
                            RatingRate = 2.6000000000000001,
                            Title = "BIYLACLESEN Women's 3-in-1 Snowboard Jacket Winter Coats"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440012"),
                            Category = "women's clothing",
                            Description = "100% POLYURETHANE(shell) 100% POLYESTER(lining) 75% POLYESTER 25% COTTON (SWEATER), Faux leather material for style and comfort",
                            Image = "https://fakestoreapi.com/img/81XH0e8fefL._AC_UY879_.jpg",
                            Price = 29.95m,
                            RatingCount = 340,
                            RatingRate = 2.8999999999999999,
                            Title = "Lock and Love Women's Removable Hooded Faux Leather Moto Biker Jacket"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440013"),
                            Category = "women's clothing",
                            Description = "Lightweight perfect for trip or casual wear. Long sleeve with hooded, adjustable drawstring waist design. Button and zipper front closure raincoat, fully stripes Lined and The Raincoat has 2 side pockets.",
                            Image = "https://fakestoreapi.com/img/71HblAHs5xL._AC_UY879_-2.jpg",
                            Price = 39.99m,
                            RatingCount = 679,
                            RatingRate = 3.7999999999999998,
                            Title = "Rain Jacket Women Windbreaker Striped Climbing Raincoats"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440014"),
                            Category = "women's clothing",
                            Description = "95% RAYON 5% SPANDEX, Made in USA or Imported, Do Not Bleach, Lightweight fabric with great stretch for comfort, Ribbed on sleeves and neckline",
                            Image = "https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_.jpg",
                            Price = 9.85m,
                            RatingCount = 130,
                            RatingRate = 4.7000000000000002,
                            Title = "MBJ Women's Solid Short Sleeve Boat Neck V"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440015"),
                            Category = "women's clothing",
                            Description = "100% Polyester, Machine wash, neck type t-shirt, Machine Wash & Pre Shrunk for a Great Fit, Lightweight, roomy and highly breathable with moisture wicking fabric",
                            Image = "https://fakestoreapi.com/img/51eg55uWmdL._AC_UX679_.jpg",
                            Price = 7.95m,
                            RatingCount = 146,
                            RatingRate = 4.5,
                            Title = "Opna Women's Short Sleeve Moisture"
                        });
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(5,2)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Sales", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.SaleItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("SaleId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("SaleId");

                    b.ToTable("SaleItems", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.CartProduct", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Cart", "Cart")
                        .WithMany("Products")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.SaleItem", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Sale", "Sale")
                        .WithMany("Items")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.User", b =>
                {
                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.Entities.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("City");

                            b1.Property<int>("Number")
                                .HasColumnType("integer")
                                .HasColumnName("Number");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("Street");

                            b1.Property<string>("Zipcode")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("Zipcode");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.OwnsOne("Ambev.DeveloperEvaluation.Domain.Entities.Geolocation", "Geolocation", b2 =>
                                {
                                    b2.Property<Guid>("AddressUserId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Lat")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .HasColumnType("character varying(20)")
                                        .HasColumnName("Latitude");

                                    b2.Property<string>("Long")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .HasColumnType("character varying(20)")
                                        .HasColumnName("Longitude");

                                    b2.HasKey("AddressUserId");

                                    b2.ToTable("Users");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressUserId");
                                });

                            b1.Navigation("Geolocation")
                                .IsRequired();
                        });

                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.Entities.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("Firstname")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("Lastname")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Cart", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sale", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
