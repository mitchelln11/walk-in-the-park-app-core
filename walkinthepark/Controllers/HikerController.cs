using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using walkinthepark.Data;
using walkinthepark.Models;

namespace walkinthepark.Controllers
{
    public class HikerController : Controller
    {
        private ApplicationDbContext _context;
        public HikerController(ApplicationDbContext context)
        {
            _context = context;
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
            catch(Exception ex)
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
                return RedirectToAction("Details", "Hiker", new { id = hiker.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: HikerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HikerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Hiker hiker)
        {
            try
            {
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
            return View();
        }

        // POST: HikerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Hiker hiker)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public string FindRegisteredUserId()
        {
            var userApplicationId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Console.WriteLine(userApplicationId);
            return userApplicationId;
            //Hiker hiker = new Hiker();
            //hiker.Id = _context.Hikers.Where(a => a.Id ==)
            //var userId = user?.Id;
            //return userId;
        }
    }
}
