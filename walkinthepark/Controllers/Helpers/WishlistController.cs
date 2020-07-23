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
        private readonly IHikerService _hikerService;
        public WishlistController(IWishlistService wishlistService, IHikerService hikerService)
        {
            _wishlistService = wishlistService;
            _hikerService = hikerService;
        }

        public ActionResult AddParkToWishListHelper(int id)
        {
            Hiker registeredHiker = _hikerService.GetLoggedInHikerId(); // Can I just directly use this for redirect?
            _wishlistService.AddParktoWishlist(id); // Add Park to wishlist
            var hikerInWishlist = _wishlistService.HikerIdFromWishlist(registeredHiker.HikerId); // find id of hiker to redirect to details
            return RedirectToAction("Details", "Hiker", new { id = hikerInWishlist.HikerId });
        }
    }
}
