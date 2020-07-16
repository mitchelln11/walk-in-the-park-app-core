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
        //private readonly HikingTrailService _trailService;
        //private readonly IConfiguration _configuration;
        //private readonly IHttpClientFactory _clientFactory;
        //RestApiNationalParks restParks;
        //public string errorString = null;

        // Need constructor with parameter to work in Core
        public ParkController(IParkService parkService/*, HikingTrailService trailService, IConfiguration configuration, IHttpClientFactory clientFactory*/)
        {
            _parkService = parkService;
            //_trailService = trailService;
            //_configuration = configuration;
            //_clientFactory = clientFactory;
        }

        // GET: ParkController

        // This works -- commented below
        //public ActionResult Index()
        //{
        //    var parks = _context.Parks.ToList();
        //    return View(parks);
        //}
        public ActionResult Index()
        {
            var parks = _parkService.GetParks();

            return View(parks);
        }

        // GET: ParkController/Details/5
        //public ActionResult Details(int id)
        //{
        //    Park Park = _parkService.GetParkRecord(id); // Get park of specific ID
        //    //var trailList = _trailService.GetTrails().Where(i => i.ParkId == id).ToList(); // Get trails that belong to Park with passed ID (Already in database)
        //    Park.CurrentWeatherInfo = new CurrentWeatherInfo(); // Instantiate blank spot for data to bind to, sets to object
        //    //await _parkService.FetchWeatherApi(Park);
        //    return View(Park);
        //}

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
        public ActionResult Delete()
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


        ////////---------------- WEATHER --------------------/////////////////
        //public async Task<RedirectToActionResult> FetchWeatherApi(Park park) // Referenced on Button click
        //{
        //    var weatherKey = _configuration["OpenWeatherKey"];
        //    string url = $"https://api.openweathermap.org/data/2.5/weather?lat={park.ParkLatitude}&lon={park.ParkLongitude}&APPID={weatherKey}";
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    string jsonresult = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        RestApiOpenWeather weather = JsonConvert.DeserializeObject<RestApiOpenWeather>(jsonresult);
        //        park.CurrentWeatherInfo.Temperature = GetCurrentTemperature(weather.main.temp);
        //        park.CurrentWeatherInfo.Condition = weather.weather[0].main;
        //        park.CurrentWeatherInfo.Wind = Math.Round(weather.wind.speed, 2);
        //        await _context.SaveChangesAsync();
        //    }
        //    return RedirectToAction("Details", park.ParkId);
        //}

        //public double GetCurrentTemperature(double kelvin)
        //{
        //    double convertKelvinToFahrenheit = Convert.ToDouble(((kelvin - 273.15) * 9 / 5) + 32);
        //    CurrentWeatherInfo currentWeather = new CurrentWeatherInfo
        //    {
        //        Temperature = Math.Round(convertKelvinToFahrenheit, 2)
        //    };
        //    try
        //    {
        //        return currentWeather.Temperature;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        _context.SaveChangesAsync();
        //    }
        //    return currentWeather.Temperature;
        //}

        //public int FindParkId(int id)
        //{
        //    Park parkId = _context.Parks.Where(p => p.ParkId == id).FirstOrDefault();
        //    return parkId.ParkId;
        //}
    }
}
