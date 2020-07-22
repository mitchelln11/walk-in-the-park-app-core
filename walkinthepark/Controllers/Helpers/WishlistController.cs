using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using walkinthepark.Models;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Controllers.Helpers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public ActionResult AddParkToWishListHelper(int id)
        {
            _wishlistService.AddParktoWishlist(id); // Add Park to wishlist
            var registeredHiker = _wishlistService.HikerIdFromWishlist(id); // find id of hiker to redirect to details
            return RedirectToAction("Details", "Hiker", new { id = registeredHiker.HikerId });
        }
    }
}
