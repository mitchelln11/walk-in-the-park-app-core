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
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRestCallsService _restCalls;
        private readonly IHikingTrailService _hikingTrails;

        // Need constructor with parameter to work in Core
        public HikingTrailController(ApplicationDbContext context, IConfiguration configuration, IRestCallsService restCalls, IHikingTrailService hikingTrails)
        {
            _context = context;
            _configuration = configuration;
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
        //public async Task<RedirectToActionResult> FetchTrailsApi(int id) // Referenced on Button click
        //{
        //    Park park = await _context.Parks.FindAsync(id);
        //    var hikingTrailsKey = _configuration["HikingProjectKey"];
        //    string url = $"https://www.hikingproject.com/data/get-trails?lat={park.ParkLatitude}&lon={park.ParkLongitude}&maxDistance=100&key={hikingTrailsKey}";
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    string jsonresult = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {

        //        RestApiHikingProject trails = JsonConvert.DeserializeObject<RestApiHikingProject>(jsonresult);
        //        List<Trail> trailInfo = trails.trails.ToList();
        //        await _context.SaveChangesAsync();
        //    }
        //    return RedirectToAction("Details", "Park", new { id = park.ParkId });
        //}

        //public async Task ApplyHikingTrailValues(Park park, List<Trail> trailInfo)
        //{
        //    foreach (var individualTrail in trailInfo)
        //    {
        //        HikingTrail hikingTrail = new HikingTrail
        //        {
        //            TrailName = individualTrail.name,
        //            TrailLength = Math.Round(individualTrail.length, 2),
        //            TrailDifficulty = individualTrail.difficulty,
        //            HikingApiCode = individualTrail.id,
        //            ParkId = park.ParkId
        //        };

        //        string trailSummary = individualTrail.summary;
        //        if (trailSummary == null)
        //        {
        //            hikingTrail.TrailSummary = "No information available at this time.";
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            hikingTrail.TrailSummary = trailSummary;
        //            await _context.SaveChangesAsync();
        //        }

        //        // Trail Conditions
        //        string trailCondition = individualTrail.conditionDetails;
        //        if (trailCondition != null)
        //        {
        //            hikingTrail.TrailCondition = trailCondition;
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            hikingTrail.TrailCondition = "No condition status available at this time";
        //            await _context.SaveChangesAsync();
        //        }

        //        // Check to see if it already exists before adding to database
        //        var trailCode = _context.HikingTrails.Where(c => c.HikingApiCode == hikingTrail.HikingApiCode).FirstOrDefault();
        //        if (trailCode == null)
        //        {
        //            _context.HikingTrails.Add(hikingTrail);
        //        }
        //        await _context.SaveChangesAsync();
        //    }
        //    await _context.SaveChangesAsync();
        //}
    }
}
