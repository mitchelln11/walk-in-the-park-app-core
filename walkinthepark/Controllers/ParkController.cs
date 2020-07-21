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

        //////////---------------- PARKS --------------------/////////////////
        /// DI attempt
        //protected async Task FetchParkApi()
        //{
        //    var parkKey = _configuration["NpsKey"];
        ////var request = new HttpRequestMessage(HttpMethod.Get,
        ////"https://developer.nps.gov/api/v1/parks?q=National%20Park&limit=91&api_key={NpsKey}");

        //// Examples below if you ever need headers for your REST API
        ////request.Headers.Add("Accept", "application / vnd.github.v3 + json");
        ////request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

        //var client = _clientFactory.CreateClient("parks");

        //    //var response = await client.SendAsync(request);

        //    try
        //    {
        //        // Base coming from the create client, referenced on startup and appsettings
        //        restParks = await client.GetFromJsonAsync<RestApiNationalParks>($"?q=National%20Park&limit=91&api_key={parkKey}");
        //        errorString = null;
        //    }
        //    catch(Exception ex)
        //    {
        //        errorString = $"There was an error with the REST API: { ex.Message }";
        //    }
        //}

        //public async Task<RedirectToActionResult> FetchParkApi() // Referenced on Button click
        //{
        //    var parkKey = _configuration["NpsKey"];
        //    string url = $"https://developer.nps.gov/api/v1/parks?q=National%20Park&limit=91&api_key={parkKey}";
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    string jsonresult = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        RestApiNationalParks parkInfo = JsonConvert.DeserializeObject<RestApiNationalParks>(jsonresult); // Run through entire JSON file
        //        var parkList = parkInfo.Data.Select(m => m).ToList(); // returns entire list of Parks, up to 150, this only has 90 based of the National Parks query

        //        foreach (var singlePark in parkList.Where(p => p.Designation.Contains("National and State Parks") || p.Designation.Contains("National Park")))
        //        {
        //            Park park = new Park
        //            {
        //                ParkName = singlePark.FullName,
        //                ParkState = singlePark.States,
        //                ParkDescription = singlePark.Description,
        //                ParkLatitude = singlePark.Latitude,
        //                ParkLongitude = singlePark.Longitude,
        //                ParkCode = singlePark.ParkCode
        //            }; // Instantiate a new park so you can use it in this method

        //            var uniqueParkCode = _context.Parks.Where(c => c.ParkCode == singlePark.ParkCode).FirstOrDefault();
        //            if (uniqueParkCode == null)
        //            {
        //                _context.Parks.Add(park);
        //                _context.SaveChanges(); // After running: "SqlException: Cannot insert explicit value for identity column in table 'Parks' when IDENTITY_INSERT is set to OFF."
        //            }
        //            await _context.SaveChangesAsync();
        //        }
        //        await _context.SaveChangesAsync();
        //    }
        //    return RedirectToAction("Index", _context.Parks.ToList());
        //}

        //public string GetGoogleMapsJsKey()
        //{
        //    var googleMapsJsKey = _configuration["GoogleMapsJsKey"];
        //    return "https://maps.googleapis.com/maps/api/js?key={googleMapsJsKey}&callback=initMapgoogleMapsJsKey";
        //}
    }
}
