using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResortBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBooking.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        { 
        }
        public DbSet<Resort> Resorts { get; set; }
        public DbSet<ResortNumber> ResortNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Resort>().HasData(
                new Resort
                {
                    Id = 1,
                    Name = "Royal Resort",
                    Description = "It is a beautiful resort with good and nice free space.",
                    ImageUrl = "images/no image.png",
                    Occupancy = 4,
                    Price = 200,
                    Sqft = 550,

                },
                new Resort
                {
                    Id = 2,
                    Name = "Premium Resort",
                    Description = "It is a beautiful, premium resort with good and nice free space.",
                    ImageUrl = "images/no image.png",
                    Occupancy = 6,
                    Price = 500,
                    Sqft = 750,

                },
                new Resort
                {
                    Id = 3,
                    Name = "Super Resort",
                    Description = "It is a beautiful and super as a good budget friendly resort with good and nice free space.",
                    ImageUrl = "images/no image.png",
                    Occupancy = 5,
                    Price = 450,
                    Sqft = 650,

                }
                );
            modelBuilder.Entity<ResortNumber>().HasData(
                new ResortNumber
                {
                    Resort_Number = 101,
                    ResortId = 1,
                },
                 new ResortNumber
                 {
                     Resort_Number = 102,
                     ResortId = 1,
                 },
                 new ResortNumber
                 {
                     Resort_Number = 103,
                     ResortId = 1,
                 },
                  new ResortNumber
                  {
                      Resort_Number = 201,
                      ResortId = 2,
                  },
                   new ResortNumber
                   {
                       Resort_Number = 202,
                       ResortId = 2,
                   },
                    new ResortNumber
                    {
                        Resort_Number = 301,
                        ResortId = 3,
                    },
                     new ResortNumber
                     {
                         Resort_Number = 302,
                         ResortId = 3,
                     });
                        modelBuilder.Entity<Amenity>().HasData(
       new Amenity
       {
           Id = 1,
           ResortId = 1,
           Name = "Table fan"
       },
       new Amenity
       {
           Id = 2,
           ResortId = 1,
           Name = "1 King bed"
       },
       new Amenity
       {
           Id = 3,
           ResortId = 1,
           Name = "3 sofa beds"
       },
       new Amenity
       {
           Id = 4,
           ResortId = 1,
           Name = "Mini Referigerator"
       },
       new Amenity
       {
           Id = 5,
           ResortId = 1,
           Name = "2 Double beds"
       },
       new Amenity
       {
           Id = 6,
           ResortId = 2,
           Name = "Private plunge pool"
       },
       new Amenity
       {
           Id = 7,
           ResortId = 2,
           Name = "Private Balcony"
       },
       new Amenity
       {
           Id = 8,
           ResortId = 3,
           Name = "Referigerator"
       },
       new Amenity
       {
           Id = 9,
           ResortId = 2,
           Name = "Microwave"
       }

                     //new ResortNumber
                     //{
                     //    Resort_Number = 402,
                     //    ResortId = 4,
                     //}
                     );
            
        }

    }
}
