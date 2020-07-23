using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using walkinthepark.Data;
using walkinthepark.Data.Migrations;
using walkinthepark.Models;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Controllers
{
    public class HikerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHikerService _hikerService;
        private readonly IWishlistService _wishlistService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HikerController(ApplicationDbContext context, IHikerService hikerService, IWishlistService wishlistService, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _hikerService = hikerService;
            _wishlistService = wishlistService;
            _signInManager = signInManager;
            _context.Database.EnsureCreated();
        }

        // GET: HikerController
        public ActionResult Index()
        {
            try
            {
                var hikers = _hikerService.GetHikers();
                return View(hikers);
            }
            catch(Exception ex) 
            {
                var hikerError = ex.Message;
                return View(hikerError);
            }
            
        }

        // GET: HikerController/Details/5
        public IActionResult Details(int? id)
        {
            Hiker Hiker = _context.Hikers.Find(id);
            try
            {
                Hiker.Wishlists = _wishlistService.GetWishlist(Hiker.HikerId);
                RedirectToRoute("Details", new { id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(Hiker);
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
                _hikerService.AddHiker(hiker);
                return RedirectToAction("Details", "Hiker", new { id = hiker.HikerId });
            }
            catch
            {
                return View();
            }
        }

        // GET: HikerController/Edit/5
        public ActionResult Edit(int id)
        {
            Hiker hiker = _hikerService.GetHikerRecord(id);
            return View(hiker);
        }

        // POST: HikerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hiker hiker)
        {
            try
            {
                _hikerService.EditHiker(hiker);
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
            Hiker hiker = _hikerService.GetHikerRecord(id);
            try
            {
                RedirectToRoute("Delete", new { id = hiker.HikerId });
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
            try
            {
                Hiker hiker = _hikerService.GetHikerRecord(id);
                IdentityUser user = _context.Users.Where(s => s.Id == hiker.ApplicationId).FirstOrDefault();
                List<HikerParkWishlist> wishlist = _wishlistService.GetWishlist(hiker.HikerId).ToList();
                if (wishlist.Count > 0)
                {
                    _wishlistService.DeleteWishlist(hiker.HikerId);
                }
                _hikerService.DeleteHiker(hiker.HikerId);
                _hikerService.DeleteApplicant(user.Id);
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
    }
}
