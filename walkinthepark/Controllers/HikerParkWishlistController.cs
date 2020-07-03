using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using walkinthepark.Data;
using walkinthepark.Models;

namespace walkinthepark.Controllers
{
    public class HikerParkWishlistController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public HikerParkWishlistController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public int FindParkId(int id)
        {
            Park parkId = _context.Parks.Where(p => p.ParkId == id).FirstOrDefault();
            return parkId.ParkId;
        }

        public int FindHikerId(int id)
        {
            Hiker hiker = _context.Hikers.Where(h => h.Id == id).FirstOrDefault();
            return hiker.Id;
        }

        public async Task<ActionResult> AddParkToWishList()
        {
            // New Wishlist item
            HikerParkWishlistViewModel wishlist = new HikerParkWishlistViewModel();
            Hiker hiker = new Hiker();
            Park park = new Park();
            try
            {
                // Find user ID to add to wishlist
                var currentUserId = FindHikerId(hiker.Id);

                // Find Park Id
                var currentParkId = FindParkId(park.ParkId);

                wishlist.HikerId = currentUserId;
                wishlist.ParkId = currentParkId;
                _context.HikerParkWishlists.Add(wishlist);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Details", "Park");
        }
    }
}
