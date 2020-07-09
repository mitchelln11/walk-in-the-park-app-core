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
        public DbSet<HikerParkWishlist> HikerParkWishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HikerParkWishlist>()
                .HasKey(t => new { t.HikerId, t.ParkId }); // Have to add unique id to Junction Table

            modelBuilder.Entity<HikerParkWishlist>()
                .HasOne(h => h.Hiker)
                .WithMany(l => l.Wishlists)
                .HasForeignKey(hi => hi.HikerId); // Matches HasKey

            modelBuilder.Entity<HikerParkWishlist>()
                .HasOne(p => p.Park)
                .WithMany(w => w.Wishlists)
                .HasForeignKey(pi => pi.ParkId); // Matches HasKey
        }

        public ApplicationDbContext()
        {

        }
    }
}
