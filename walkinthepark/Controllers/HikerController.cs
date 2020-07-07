using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using walkinthepark.Data;
using walkinthepark.Models;
using walkinthepark.ViewModels;

namespace walkinthepark.Controllers
{
    public class HikerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HikerController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: HikerController
        public ActionResult Index()
        {
            return View(_context.Hikers.ToList());
        }

        // GET: HikerController/Details/5
        public ActionResult Details(int id)
        {
            Hiker hiker = _context.Hikers.Find(id);
            try
            {
                RedirectToRoute("Details", new { id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(hiker);
        }

        // GET: HikerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HikerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] Hiker hiker)
        {
            try
            {
                hiker.ApplicationId = FindRegisteredUserId();
                _context.Hikers.Add(hiker);
                _context.SaveChanges();
                return RedirectToAction("Details", "Hiker", new { id = hiker.HikerId });
            }
            catch
            {
                return View();
            }
        }

        // GET: HikerController/Edit/5
        public ActionResult Edit(int? id)
        {
            Hiker hiker = _context.Hikers.Find(id);
            return View(hiker);
        }

        // POST: HikerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Hiker hiker)
        {
            try
            {
                _context.Hikers.Update(hiker);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HikerController/Delete/5
        public ActionResult Delete(int id)
        {
            Hiker hiker = _context.Hikers.Find(id);
            try
            {
                RedirectToRoute("Delete", new { id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(hiker);
        }

        // POST: HikerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Hiker hiker = _context.Hikers.Find(id);
            IdentityUser user = _context.Users.Where(s => s.Id == hiker.ApplicationId).FirstOrDefault();
            try
            {
                _context.Hikers.Remove(hiker);
                _context.SaveChanges();
                _context.Users.Remove(user);
                _context.SaveChanges();
                await LogOutUser();
                return RedirectToAction("Index", "Park");
            }
            catch
            {
                return View();
            }
        }

        // Find Id of logged in user
        public string FindRegisteredUserId() => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        // Log out user
        public async Task<IActionResult> LogOutUser()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("DeleteConfirmed");
        }

        public async Task<ActionResult> AddParkToWishList(int id)
        {

            // Find logged in user to add to the correct wishlist
            string currentUser = FindRegisteredUserId();
            var currentHiker = _context.Hikers.Where(h => h.ApplicationId == currentUser).FirstOrDefault();

            // Find current park
            var currentPark = _context.Parks.Where(p => p.ParkId == id).FirstOrDefault();

            HikerParkWishlist wishlist = new HikerParkWishlist()
            {
                HikerId = currentHiker.HikerId,
                ParkId = currentPark.ParkId,
                ParkName = currentPark.ParkName
            };

            _context.HikerParkWishlists.Add(wishlist);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Hiker", new { id = currentHiker.HikerId });
        }
    }
}
