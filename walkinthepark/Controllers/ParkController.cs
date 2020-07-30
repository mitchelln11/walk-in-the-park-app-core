using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using walkinthepark.Data;
using walkinthepark.Models;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Microsoft.AspNetCore.Diagnostics;
using walkinthepark.Services.Interfaces;
using walkinthepark.Services;
using System.Net.Http.Json;

namespace walkinthepark.Controllers
{
    public class ParkController : Controller
    {
        private readonly IParkService _parkService;
        private readonly IHikingTrailService _trailService;
        private readonly IRestCallsService _restCalls;

        // Need constructor with parameter to work in Core
        public ParkController(IParkService parkService, IHikingTrailService trailService, IRestCallsService restCalls)
        {
            _parkService = parkService;
            _trailService = trailService;
            _restCalls = restCalls;
        }

        // GET: ParkController
        public ActionResult Index()
        {
            var parks = _parkService.GetParks();
            return View(parks);
        }

        // GET: ParkController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Park Park = _parkService.GetParkRecord(id); // Get park of specific ID
            Park.HikingTrail = _trailService.GetTrails(id); // Get trails that belong to Park with passed ID (Already in database)
            Park.CurrentWeatherInfo = new CurrentWeatherInfo(); // Instantiate blank spot for data to bind to, sets to object
            await _restCalls.FetchWeatherApi(Park);
            return View(Park);
        }

        // GET: ParkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Park park)
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

        // GET: ParkController/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: ParkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Park park)
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

        // GET: ParkController/Delete/5
        public ActionResult Delete(int id)
        {
            Park Park = _parkService.GetParkRecord(id);
            try
            {
                RedirectToRoute("Delete", new { id = Park.ParkId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(Park);
        }

        // POST: ParkController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Park Park = _parkService.GetParkRecord(id);
                _trailService.DeleteTrails(Park);
                _parkService.DeletePark(Park.ParkId);
                return RedirectToAction("Index", "Park");
            }
            catch
            {
                return RedirectToAction("Index", "Park");
            }
        }

        /// <summary>
        /// ------------ HELPER METHODS ----------
        /// </summary>
        public ActionResult FilterMultiStateParks()
        {
            _parkService.GetStatesWithParks();
            return RedirectToAction("Index", "Park");
        }

        public ActionResult ResetParkList()
        {
            _parkService.GetParks();
            return RedirectToAction("Index", "Park");
        }
    }
}
