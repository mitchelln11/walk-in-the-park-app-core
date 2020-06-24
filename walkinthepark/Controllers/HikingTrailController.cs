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

namespace walkinthepark.Controllers
{
    public class HikingTrailController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        // Need constructor with parameter to work in Core
        public HikingTrailController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: HikingTrailController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HikingTrailController/Details/5
        public ActionResult Details(int id)
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
        public ActionResult Edit(int id)
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
        public ActionResult Delete(int id)
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

        ////////---------------- HIKING TRAILS --------------------/////////////////
        public async Task<RedirectToActionResult> FetchTrailsApi(int id) // Referenced on Button click
        {
            Park park = await _context.Parks.FindAsync(id);
            var hikingTrailsKey = _configuration["HikingProjectKey"];
            string url = $"https://www.hikingproject.com/data/get-trails?lat={park.ParkLatitude}&lon={park.ParkLongitude}&maxDistance=100&key={hikingTrailsKey}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonresult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                RestApiHikingProject trails = JsonConvert.DeserializeObject<RestApiHikingProject>(jsonresult);
                List<Trail> trailInfo = trails.trails.ToList();
                await ApplyHikingTrailValues(park, trailInfo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Park", new { id = park.ParkId });
        }

        public async Task ApplyHikingTrailValues(Park park, List<Trail> trailInfo)
        {
            var foreignParkId = park.ParkId;
            //HikingTrail hiking = _context.HikingTrails.Where(t => t.ParkId == foreignParkId).FirstOrDefault();
            foreach (var individualTrail in trailInfo)
            {
                HikingTrail hikingTrail = new HikingTrail();
                hikingTrail.TrailName = individualTrail.name;
                hikingTrail.TrailLength = Math.Round(individualTrail.length, 2);
                hikingTrail.TrailDifficulty = individualTrail.difficulty;
                hikingTrail.HikingApiCode = individualTrail.id;
                hikingTrail.ParkId = park.ParkId;

                string trailSummary = hikingTrail.TrailSummary;
                if (trailSummary == null)
                {
                    hikingTrail.TrailSummary = "No information available at this time.";
                    await _context.SaveChangesAsync();
                }
                else
                {
                    hikingTrail.TrailSummary = trailSummary;
                    await _context.SaveChangesAsync();
                }

                // Trail Conditions
                string trailCondition = hikingTrail.TrailCondition;
                if (trailCondition != null)
                {
                    hikingTrail.TrailCondition = trailCondition;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    hikingTrail.TrailCondition = "No condition status available at this time";
                    await _context.SaveChangesAsync();
                }

                // Check to see if it already exists before adding to database
                var trailCode = _context.HikingTrails.Where(c => c.HikingApiCode == hikingTrail.HikingApiCode).FirstOrDefault();
                if (trailCode == null)
                {
                    _context.HikingTrails.Add(hikingTrail);
                }
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }
    }
}
