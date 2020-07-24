using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using walkinthepark.Data;
using walkinthepark.Models;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Controllers
{
    public class HikingTrailController : Controller
    {
        private readonly IRestCallsService _restCalls;
        private readonly IHikingTrailService _hikingTrails;

        // Need constructor with parameter to work in Core
        public HikingTrailController(IRestCallsService restCalls, IHikingTrailService hikingTrails)
        {
            _restCalls = restCalls;
            _hikingTrails = hikingTrails;
        }

        // GET: HikingTrailController
        public ActionResult Index()
        {
            var trails = _hikingTrails.GetTrails();
            return View(trails);
        }

        // GET: HikingTrailController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: HikingTrailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HikingTrailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: HikingTrailController/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: HikingTrailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: HikingTrailController/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: HikingTrailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        public async Task<RedirectToActionResult> FetchTrailsRestHelper(int id)
        {
            await _restCalls.FetchTrailsApi(id);
            return RedirectToAction("Details", "Park", new { id });
        }
    }
}
