using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Data;
using walkinthepark.Models;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext _context; // For database info
        private readonly ParkService _parkService; // For park methods
        private readonly HikerService _hikerService; // For hiker methods
        private readonly SignInManager<IdentityUser> _signInManager; // To find out if they're logged in

        public WishlistService(ApplicationDbContext context, ParkService parkService, HikerService hikerService, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _parkService = parkService;
            _hikerService = hikerService;
            _signInManager = signInManager;
        }

        // 1. Find ID of park when on a page.
        // 2. Find out if hiker is logged in
        // 3. Find UserApplication ID
        // 4. Find Hiker ID based on UserApplication ID


        // Find wishlist with several records that belongs to Hiker
        public List<HikerParkWishlist> GetHikerWishlist(int id)
        {
            var wishlist = _context.HikerParkWishlists.Where(w => w.HikerId == id).ToList();
            return wishlist;
        }
        //var hiker = _hikerService
        //IdentityUser user = _context.Users.Where(s => s.Id == hiker.ApplicationId).FirstOrDefault();

        // Find Id of logged in user
        //public string FindRegisteredUserId() => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        ////Add Park to Wishlist
        //public async Task<ActionResult> AddParkToWishList(int id)
        //{
        //    // Find logged in user to add to the correct wishlist
        //    string currentUser = FindRegisteredUserId();
        //    var currentHiker = _context.Hikers.Where(h => h.ApplicationId == currentUser).FirstOrDefault();

        //    // Find current park
        //    var currentPark = _context.Parks.Where(p => p.ParkId == id).FirstOrDefault();

        //    HikerParkWishlist wishlist = new HikerParkWishlist()
        //    {
        //        HikerId = currentHiker.HikerId,
        //        ParkId = currentPark.ParkId,
        //        ParkName = currentPark.ParkName
        //    };

        //    // make sure there are no duplicates
        //    bool parkExistsInWishlist = _context.HikerParkWishlists.Any(w => w.ParkId == wishlist.ParkId); // Doesn't let you add a park to wishlist if it exist on someone else's wishlist
        //    if (parkExistsInWishlist == false)
        //    {
        //        _context.HikerParkWishlists.Add(wishlist);
        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToAction("Details", "Hiker", new { id = currentHiker.HikerId });
        //}
    }
}
