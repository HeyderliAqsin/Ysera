    using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entities;


namespace DataAccess
{
    public class JewelleryDbContext : IdentityDbContext<YseraUser>
    {
        public JewelleryDbContext(DbContextOptions<JewelleryDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<MyConfiguration> MyConfigurations { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<YseraUser> YseraUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            builder.Entity<YseraUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<Category>().HasData( 
             new Category() { Id = 1, Name = "GoldJewelery", IconUrl = "fab fa-amazon" },
             new Category() { Id = 2, Name = "SilverNecklace", IconUrl = "fab fa-instagram" },
             new Category() { Id = 3, Name = "Earrings", IconUrl = "fas fa-address-card" }
           );
            builder.Entity<Product>().HasData(
            new Product()
            {
                Id = 1,
                Name = "GoldJewelery 1",
                Price = 1200,
                IsFeatured = false,
                CategoryId = 1,
                IsComplect = false,
                IsWeek = false,
                IsMonth = false,
                IsSlider = false,
                CoverPhotoId = 5,
                Discount = 200,
                IsDeleted = false,
                Rating = 4,

            },
            new Product()
            {
                Id = 2,
                Name = "GoldJewelery 2",
                Price = 1900,
                IsFeatured = false,
                CategoryId = 2,
                Discount = 500,
                IsComplect = false,
                IsWeek = false,
                IsMonth = false,
                Rating = 2,
                IsSlider = false,
                CoverPhotoId = 6,
                IsDeleted = false,
            },
              new Product()
              {
                  Id = 3,
                  Name = "GoldJewelery 2",
                  Price = 2800,
                  IsFeatured = false,
                  CategoryId = 3,
                  Discount = 600,
                  Rating = 3,
                  IsComplect = false,
                  IsWeek = false,
                  IsMonth = false,
                  IsSlider = false,
                  CoverPhotoId = 9,
                  IsDeleted = false,
              }
        );
            builder.Entity<Order>().HasData(
                 new Order()
                 {
                     Id=7,
                     CustomerName = "Amil",
                     CustomerAddress = "Baki",
                     CustomerPhone = "055555555",
                     Customeremail = "user@gmail.com",
                     CustomerId = "2",
                     OrderCode="12345a"

                 },
                  new Order()
                  {
                      Id=3,
                      CustomerName = "Akif",
                      CustomerAddress = "Seki",
                      CustomerPhone = "055555855",
                      Customeremail = "user2@gmail.com",
                      CustomerId = "3",
                      OrderCode = "123478aa"


                  },
                  new Order()
                  {
                      Id=9,
                      CustomerName = "Arzu",
                      CustomerAddress = "Quba",
                      CustomerPhone = "055555859",
                      Customeremail = "user3@gmail.com",
                      CustomerId = "4",
                      OrderCode = "d12345a"


                  }


                );


        }

    }
}