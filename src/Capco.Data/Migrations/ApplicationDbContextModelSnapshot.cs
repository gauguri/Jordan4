using System;
using Capco.Data;
using Capco.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Capco.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Capco.Domain.Entities.Address", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("City")
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnType("nvarchar(80)");

                b.Property<string>("Country")
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnType("nvarchar(60)");

                b.Property<int?>("CustomerId")
                    .HasColumnType("int");

                b.Property<string>("Line1")
                    .IsRequired()
                    .HasMaxLength(160)
                    .HasColumnType("nvarchar(160)");

                b.Property<string>("Line2")
                    .HasMaxLength(160)
                    .HasColumnType("nvarchar(160)");

                b.Property<string>("State")
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnType("nvarchar(60)");

                b.Property<string>("Type")
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnType("nvarchar(40)");

                b.Property<string>("Zip")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.HasKey("Id");

                b.HasIndex("CustomerId");

                b.ToTable("Addresses");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Cart", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<int?>("CustomerId")
                    .HasColumnType("int");

                b.Property<string>("GuestToken")
                    .HasMaxLength(64)
                    .HasColumnType("nvarchar(64)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("datetime2");

                b.HasKey("Id");

                b.HasIndex("CustomerId");

                b.ToTable("Carts");
            });

            modelBuilder.Entity("Capco.Domain.Entities.CartItem", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<int>("CartId")
                    .HasColumnType("int");

                b.Property<int>("ProductVariantId")
                    .HasColumnType("int");

                b.Property<int>("Qty")
                    .HasColumnType("int");

                b.Property<decimal>("UnitPriceSnapshot")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("Id");

                b.HasIndex("CartId");

                b.HasIndex("ProductVariantId");

                b.ToTable("CartItems");
            });

            modelBuilder.Entity("Capco.Domain.Entities.ContentBlock", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Html")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Key")
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnType("nvarchar(80)");

                b.HasKey("Id");

                b.HasIndex("Key")
                    .IsUnique();

                b.ToTable("ContentBlocks");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Customer", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("FirstName")
                    .HasMaxLength(80)
                    .HasColumnType("nvarchar(80)");

                b.Property<string>("LastName")
                    .HasMaxLength(80)
                    .HasColumnType("nvarchar(80)");

                b.Property<string>("Phone")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("UserId")
                    .IsUnique();

                b.ToTable("Customers");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Order", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<int?>("BillingAddressId")
                    .HasColumnType("int");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime2");

                b.Property<int?>("CustomerId")
                    .HasColumnType("int");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("OrderNumber")
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnType("nvarchar(32)");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnType("nvarchar(32)");

                b.Property<int?>("ShippingAddressId")
                    .HasColumnType("int");

                b.Property<decimal>("Shipping")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("StripePaymentIntentId")
                    .HasMaxLength(160)
                    .HasColumnType("nvarchar(160)");

                b.Property<decimal>("Subtotal")
                    .HasColumnType("decimal(18,2)");

                b.Property<decimal>("Tax")
                    .HasColumnType("decimal(18,2)");

                b.Property<decimal>("Total")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("Id");

                b.HasIndex("BillingAddressId");

                b.HasIndex("CustomerId");

                b.HasIndex("OrderNumber")
                    .IsUnique();

                b.HasIndex("ShippingAddressId");

                b.ToTable("Orders");
            });

            modelBuilder.Entity("Capco.Domain.Entities.OrderItem", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ColorSnapshot")
                    .HasMaxLength(40)
                    .HasColumnType("nvarchar(40)");

                b.Property<string>("NameSnapshot")
                    .IsRequired()
                    .HasMaxLength(160)
                    .HasColumnType("nvarchar(160)");

                b.Property<int>("OrderId")
                    .HasColumnType("int");

                b.Property<int>("ProductVariantId")
                    .HasColumnType("int");

                b.Property<int>("Qty")
                    .HasColumnType("int");

                b.Property<string>("SizeSnapshot")
                    .HasMaxLength(60)
                    .HasColumnType("nvarchar(60)");

                b.Property<decimal>("UnitPriceSnapshot")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("Id");

                b.HasIndex("OrderId");

                b.HasIndex("ProductVariantId");

                b.ToTable("OrderItems");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Product", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Collection")
                    .HasMaxLength(80)
                    .HasColumnType("nvarchar(80)");

                b.Property<DateTime>("CreatedAt")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETUTCDATE()");

                b.Property<string>("Description")
                    .HasMaxLength(4000)
                    .HasColumnType("nvarchar(4000)");

                b.Property<bool>("IsActive")
                    .HasColumnType("bit");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(160)
                    .HasColumnType("nvarchar(160)");

                b.Property<string>("Slug")
                    .IsRequired()
                    .HasMaxLength(160)
                    .HasColumnType("nvarchar(160)");

                b.HasKey("Id");

                b.HasIndex("Slug")
                    .IsUnique();

                b.ToTable("Products");
            });

            modelBuilder.Entity("Capco.Domain.Entities.ProductImage", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<bool>("IsPrimary")
                    .HasColumnType("bit");

                b.Property<int>("ProductId")
                    .HasColumnType("int");

                b.Property<int>("SortOrder")
                    .HasColumnType("int");

                b.Property<string>("Alt")
                    .HasMaxLength(160)
                    .HasColumnType("nvarchar(160)");

                b.Property<string>("Url")
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnType("nvarchar(512)");

                b.HasKey("Id");

                b.HasIndex("ProductId");

                b.ToTable("ProductImages");
            });

            modelBuilder.Entity("Capco.Domain.Entities.ProductVariant", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<bool>("AllowBackorder")
                    .HasColumnType("bit");

                b.Property<string>("Color")
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnType("nvarchar(40)");

                b.Property<decimal?>("CompareAtPrice")
                    .HasColumnType("decimal(18,2)");

                b.Property<int>("InventoryQty")
                    .HasColumnType("int");

                b.Property<bool>("IsActive")
                    .HasColumnType("bit");

                b.Property<decimal>("Price")
                    .HasColumnType("decimal(18,2)");

                b.Property<int>("ProductId")
                    .HasColumnType("int");

                b.Property<string>("SizeLabel")
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnType("nvarchar(60)");

                b.Property<string>("Sku")
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnType("nvarchar(60)");

                b.Property<string>("UPC")
                    .HasMaxLength(32)
                    .HasColumnType("nvarchar(32)");

                b.Property<int>("WeightGrams")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("ProductId");

                b.HasIndex("Sku")
                    .IsUnique();

                b.ToTable("ProductVariants");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Order", b =>
            {
                b.HasOne("Capco.Domain.Entities.Address", "BillingAddress")
                    .WithMany()
                    .HasForeignKey("BillingAddressId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne("Capco.Domain.Entities.Customer", "Customer")
                    .WithMany("Orders")
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne("Capco.Domain.Entities.Address", "ShippingAddress")
                    .WithMany("Orders")
                    .HasForeignKey("ShippingAddressId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.Navigation("BillingAddress");

                b.Navigation("Customer");

                b.Navigation("ShippingAddress");
            });

            modelBuilder.Entity("Capco.Domain.Entities.OrderItem", b =>
            {
                b.HasOne("Capco.Domain.Entities.Order", "Order")
                    .WithMany("Items")
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Capco.Domain.Entities.ProductVariant", "Variant")
                    .WithMany()
                    .HasForeignKey("ProductVariantId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Order");

                b.Navigation("Variant");
            });

            modelBuilder.Entity("Capco.Domain.Entities.ProductImage", b =>
            {
                b.HasOne("Capco.Domain.Entities.Product", "Product")
                    .WithMany("Images")
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Product");
            });

            modelBuilder.Entity("Capco.Domain.Entities.ProductVariant", b =>
            {
                b.HasOne("Capco.Domain.Entities.Product", "Product")
                    .WithMany("Variants")
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Product");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Address", b =>
            {
                b.HasOne("Capco.Domain.Entities.Customer", "Customer")
                    .WithMany("Addresses")
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.Navigation("Customer");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Cart", b =>
            {
                b.HasOne("Capco.Domain.Entities.Customer", "Customer")
                    .WithMany()
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.Navigation("Customer");
            });

            modelBuilder.Entity("Capco.Domain.Entities.CartItem", b =>
            {
                b.HasOne("Capco.Domain.Entities.Cart", "Cart")
                    .WithMany("Items")
                    .HasForeignKey("CartId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Capco.Domain.Entities.ProductVariant", "Variant")
                    .WithMany()
                    .HasForeignKey("ProductVariantId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Cart");

                b.Navigation("Variant");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Customer", b =>
            {
                b.HasOne("Capco.Domain.Identity.ApplicationUser", null)
                    .WithOne()
                    .HasForeignKey("Capco.Domain.Entities.Customer", "UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Capco.Domain.Identity.ApplicationUser", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("int");

                b.Property<string>("ConcurrencyStamp")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("bit");

                b.Property<string>("FirstName")
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("LastName")
                    .HasColumnType("nvarchar(256)");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("bit");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("datetimeoffset");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("bit");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("bit");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("AspNetUsers", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ConcurrencyStamp")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                b.ToTable("AspNetRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("ProviderKey")
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("RoleId")
                    .HasColumnType("nvarchar(450)");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("LoginProvider")
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Value")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", (string)null);
            });

            modelBuilder.Entity("Capco.Domain.Entities.Cart", b =>
            {
                b.Navigation("Items");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Customer", b =>
            {
                b.Navigation("Addresses");

                b.Navigation("Orders");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Order", b =>
            {
                b.Navigation("Items");
            });

            modelBuilder.Entity("Capco.Domain.Entities.Product", b =>
            {
                b.Navigation("Images");

                b.Navigation("Variants");
            });
#pragma warning restore 612, 618
        }
    }
}
