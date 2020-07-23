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

        public WishlistService(ApplicationDbContext context, IParkService parkService, IHikerService hikerService)
        {
            _context = context;
            _parkService = parkService;
            _hikerService = hikerService;
        }
        public List<HikerParkWishlist> GetWishlist() => _context.HikerParkWishlists.ToList();
        public List<HikerParkWishlist> GetWishlist(int id) => _context.HikerParkWishlists.Where(t => t.HikerId == id).ToList();

        public HikerParkWishlist HikerIdFromWishlist(int id) => _context.HikerParkWishlists.Where(h => h.ParkId == id).FirstOrDefault();
        public void AddParktoWishlist(int id)
        { // Redirecting to original person who added a park to the wishlist
            try
            {
                var currentPark = _parkService.GetParkRecord(id);

                var registeredHiker = _hikerService.FindRegisteredUserId();
                var hiker = _hikerService.GetHikers().Where(h => h.ApplicationId == registeredHiker).FirstOrDefault();

                HikerParkWishlist parkWishlist = new HikerParkWishlist
                {
                    HikerId = hiker.HikerId,
                    ParkId = currentPark.ParkId,
                    ParkName = currentPark.ParkName
                };
                _context.HikerParkWishlists.Add(parkWishlist);
                _context.SaveChanges();
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
