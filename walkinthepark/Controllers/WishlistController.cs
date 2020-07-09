using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using walkinthepark.Data;
using walkinthepark.Models;

namespace walkinthepark.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public IActionResult Index()
        {
            var model = _context.HikerParkWishlists.ToList();
            return View(model);
        }

        public IActionResult IndexVC()
        {
            return ViewComponent("Wishlist", new HikerParkWishlist());
        }
    }
}
