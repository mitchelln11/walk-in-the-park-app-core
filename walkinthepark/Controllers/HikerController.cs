using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            return View();
        }

        // GET: HikerController/Create
        public ActionResult Create()
        {
            Hiker hiker = new Hiker();
            return View(hiker);
        }

        // POST: HikerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hiker hiker)
        {
            try
            {
                _context.Hikers.Add(hiker);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
    }
}
