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
        private readonly IParkService _parkService; // For park methods
        private readonly IHikerService _hikerService; // For hiker methods
        private readonly IUserService _userService;
        public bool EmptyWishlist = true;

        public WishlistService(ApplicationDbContext context, IParkService parkService, IHikerService hikerService, IUserService userService)
        {
            _context = context;
            _parkService = parkService;
            _hikerService = hikerService;
            _userService = userService;
        }
        public List<HikerParkWishlist> GetWishlist() => _context.HikerParkWishlists.ToList();
        public List<HikerParkWishlist> GetWishlist(int id) => _context.HikerParkWishlists.Where(t => t.HikerId == id).ToList();
        public HikerParkWishlist HikerIdFromWishlist(int id) => _context.HikerParkWishlists.Where(w => w.HikerId == id).FirstOrDefault();
        
        private Hiker FindCurrentHiker() // Find AspNetUsers Application Id and match with logged in hiker
        {
            string currentHiker = _userService.FindRegisteredUserId();
            Hiker hiker = _hikerService.GetHikers().Where(h => h.ApplicationId == currentHiker).FirstOrDefault();
            return hiker;
        }

        public void CheckEmptyWishlist()
        {
            Hiker currentHiker = FindCurrentHiker();
            List<HikerParkWishlist> currentHikerWishlist = GetWishlist(currentHiker.HikerId);

            int emptyWishlist = currentHikerWishlist.Count();
            if (emptyWishlist > 0)
            {
                currentHiker.EmptyWishlist = false;
                _context.Hikers.Update(currentHiker);
                _context.SaveChangesAsync();
            }
            else
            {
                currentHiker.EmptyWishlist = true;
                _context.Hikers.Update(currentHiker);
                _context.SaveChangesAsync();
            }
        }

        // Data Manipulation
        public void AddParktoWishlist(int id)
        { // Redirecting to original person who added a park to the wishlist
            try
            {
                var currentPark = _parkService.GetParkRecord(id);
                var currentHiker = FindCurrentHiker();

                List<HikerParkWishlist> currentHikerWishlist = GetWishlist(currentHiker.HikerId);

                HikerParkWishlist parkWishlist = new HikerParkWishlist
                {
                    HikerId = currentHiker.HikerId,
                    ParkId = currentPark.ParkId,
                    ParkName = currentPark.ParkName
                };

                // If any records in the current wishlit do not contain the Park name (Prevent Duplicate)
                if (!currentHikerWishlist.Any(p => p.ParkName.Contains(parkWishlist.ParkName)))
                {
                    _context.HikerParkWishlists.Add(parkWishlist);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteWishlist(int id)
        {
            try
            {
                List<HikerParkWishlist> wishlistToRemove = _context.HikerParkWishlists.Where(h => h.HikerId == id).ToList();
                foreach(var park in wishlistToRemove)
                {
                    _context.HikerParkWishlists.Remove(park);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
