using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using walkinthepark.Models;

namespace walkinthepark.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Hiker> Hikers { get; set; }
        public DbSet<Park> Parks { get; set; }
        public DbSet<HikingTrail> HikingTrails { get; set; }
        public DbSet<HikerParkWishlistViewModel> HikerParkWishlists { get; set; }
        public DbSet<HikerTrailRatingViewModel> HikerTrailRatings { get; set; }


        public ApplicationDbContext()
        {

        }
    }
}
