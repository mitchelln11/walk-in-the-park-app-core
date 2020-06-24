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

namespace walkinthepark.Controllers
{
    public class ParkController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        // Need constructor with parameter to work in Core
        public ParkController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: ParkController
        public ActionResult Index()
        {
            return View(_context.Parks.ToList());
        }

        // GET: ParkController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Park park = await _context.Parks.FindAsync(id);
            park.HikingTrail = _context.HikingTrails.Where(i => i.ParkId == id).ToList();
            //park.CurrentWeatherInfo = new CurrentWeatherInfo(); // Instantiate park to bind data to
            //await FetchWeatherApi(park);
            //await FetchTrailsApi(park);
            return View(park);
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
        public ActionResult Edit(int id)
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
            return View();
        }

        // POST: ParkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Park park)
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

        ////////---------------- PARKS --------------------/////////////////
        public async Task<RedirectToActionResult> FetchParkApi() // Referenced on Button click
        {
            var parkKey = _configuration["NpsKey"];
            string url = $"https://developer.nps.gov/api/v1/parks?q=National%20Park&limit=91&api_key={parkKey}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonresult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                RestApiNationalParks parkInfo = JsonConvert.DeserializeObject<RestApiNationalParks>(jsonresult); // Run through entire JSON file
                var parkList = parkInfo.data.Select(m => m).ToList(); // returns entire list of Parks, up to 150, this only has 90 based of the National Parks query

                foreach (var singlePark in parkList.Where(p => p.designation.Contains("National and State Parks") || p.designation.Contains("National Park")))
                {
                    Park park = new Park(); // Instantiate a new park so you can use it in this method
                    park.ParkName = singlePark.fullName;
                    park.ParkState = singlePark.states;
                    park.ParkDescription = singlePark.description;
                    park.ParkLatitude = singlePark.latitude;
                    park.ParkLongitude = singlePark.longitude;
                    park.ParkCode = singlePark.parkCode;

                    var uniqueParkCode = _context.Parks.Where(c => c.ParkCode == singlePark.parkCode).FirstOrDefault();
                    if (uniqueParkCode == null)
                    {
                        _context.Parks.Add(park);
                        _context.SaveChanges(); // After running: "SqlException: Cannot insert explicit value for identity column in table 'Parks' when IDENTITY_INSERT is set to OFF."
                    }
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", _context.Parks.ToList());
        }

        public string GetGoogleMapsJsKey()
        {
            var googleMapsJsKey = _configuration["GoogleMapsJsKey"];
            string googleUrl = "https://maps.googleapis.com/maps/api/js?key={googleMapsJsKey}&callback=initMapgoogleMapsJsKey";
            return googleUrl;
        }


        ////////---------------- WEATHER --------------------/////////////////
        public async Task<RedirectToActionResult> FetchWeatherApi(Park park) // Referenced on Button click
        {
            var weatherKey = _configuration["OpenWeatherKey"];
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={park.ParkLatitude}&lon={park.ParkLongitude}&APPID={weatherKey}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonresult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                RestApiOpenWeather weather = JsonConvert.DeserializeObject<RestApiOpenWeather>(jsonresult);
                await GetCurrentTemperature(weather.main.temp);

                //string condition = weather.condition;
                //weather.wind = weather.;
                //var temperature = weather.temperature;
                Console.WriteLine();
                await _context.SaveChangesAsync();
                //await GetWindCondition(weather.wind);
            }
            return RedirectToAction("Details", park.ParkId);
        }

        public async Task GetCurrentTemperature(double kelvin)
        {
            double convertKelvinToFahrenheit = Convert.ToDouble(((kelvin - 273.15) * 9 / 5) + 32);
            CurrentWeatherInfo currentWeather = new CurrentWeatherInfo();
            currentWeather.temperature = Math.Round(convertKelvinToFahrenheit, 2);
            await _context.SaveChangesAsync();
        }

        //////////---------------- HIKING TRAILS --------------------/////////////////
        //public async Task<RedirectToActionResult> FetchTrailsApi(Park park) // Referenced on Button click
        //{
        //    var hikingTrailsKey = _configuration["HikingProjectKey"];
        //    string url = $"https://www.hikingproject.com/data/get-trails?lat={park.ParkLatitude}&lon={park.ParkLongitude}&maxDistance=100&key={hikingTrailsKey}";
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    string jsonresult = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
                
        //        RestApiHikingProject trails = JsonConvert.DeserializeObject<RestApiHikingProject>(jsonresult);
        //        List<Trail> trailInfo = trails.trails.ToList();
        //        await ApplyHikingTrailValues(park, trailInfo);
        //        await _context.SaveChangesAsync();
        //    }
        //    return RedirectToAction("Details");
        //}

        //public async Task ApplyHikingTrailValues(Park park, List<Trail> trailInfo)
        //{
        //    var foreignParkId = park.ParkId;
        //    //HikingTrail hiking = _context.HikingTrails.Where(t => t.ParkId == foreignParkId).FirstOrDefault();
        //    foreach (var individualTrail in trailInfo)
        //    {
        //        HikingTrail hikingTrail = new HikingTrail();
        //        hikingTrail.TrailName = individualTrail.name;
        //        hikingTrail.TrailLength = Math.Round(individualTrail.length, 2);
        //        hikingTrail.TrailDifficulty = individualTrail.difficulty;
        //        hikingTrail.HikingApiCode = individualTrail.id;
        //        hikingTrail.ParkId = park.ParkId;

        //        string trailSummary = hikingTrail.TrailSummary;
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
        //        string trailCondition = hikingTrail.TrailCondition;
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
