using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using walkinthepark.Data;
using walkinthepark.Models;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Services
{
    public class RestCallsService : IRestCallsService
    {
        private readonly ApplicationDbContext _context;
        private IHttpClientFactory ClientFactory { get; set; }
        private readonly IConfiguration _configuration;
        public RestApiOpenWeather RestParks { get; private set; }
        //public RestApiHikingProject RestTrails { get; private set; }
        public string GetError { get; private set; }

        public RestCallsService(ApplicationDbContext context,  IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _context = context;
            ClientFactory = clientFactory;
            _configuration = configuration;
        }


        /// <summary>
        /// WEATHER INFORMATION FROM OPENWEATHER REST API ENDPOINT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public async Task<string> FetchWeatherApi(Park park)
        {
            string weatherKey = _configuration["OpenWeatherKey"];
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.openweathermap.org/data/2.5/weather?lat={park.ParkLatitude}&lon={park.ParkLongitude}&APPID={weatherKey}");

            var client = ClientFactory.CreateClient("weather");
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                RestParks = await response.Content.ReadFromJsonAsync<RestApiOpenWeather>();
                park.CurrentWeatherInfo.Temperature = GetCurrentTemperature(Convert.ToDouble(RestParks.Main.Temp));
                park.CurrentWeatherInfo.Condition = RestParks.Weather[0].Main;
                park.CurrentWeatherInfo.Wind = Math.Round(RestParks.Wind.Speed, 2);
                await _context.SaveChangesAsync();
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"Status Code: {response.StatusCode}";
            }
        }

        public double GetCurrentTemperature(double kelvin)
        {
            double convertKelvinToFahrenheit = Convert.ToDouble(((kelvin - 273.15) * 9 / 5) + 32);
            CurrentWeatherInfo currentWeather = new CurrentWeatherInfo
            {
                Temperature = Math.Round(convertKelvinToFahrenheit, 2)
            };
            try
            {
                return currentWeather.Temperature;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _context.SaveChangesAsync();
            }
            return currentWeather.Temperature;
        }
        /// <summary>
        /// PARK INFORMATION FROM NPS REST API ENDPOINT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public async Task<string> FetchParksApi()
        {
            var parksKey = _configuration["NpsKey"];
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://developer.nps.gov/api/v1/parks?q=National%20Park&limit=91&api_key={parksKey}");

            var client = ClientFactory.CreateClient("parks");
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                RestApiNationalParks RestParks = await response.Content.ReadFromJsonAsync<RestApiNationalParks>();
                var parkInfo = RestParks.Data.Select(m => m).ToList();
                await ApplyParkValues(parkInfo);
                await _context.SaveChangesAsync();
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"Status Code: {response.StatusCode}";
            }
        }

        public async Task ApplyParkValues(List<Datum> parkInfo)
        {
            foreach (var individualPark in parkInfo)
            {
                Park park = new Park
                {
                    Designation = individualPark.Designation // Temporary National Parks holding
                };
                if (park.Designation.Contains("National and State Parks") || park.Designation.Contains("National Park")) // First statement to add Redwood
                {
                    park.ParkName = individualPark.FullName;
                    park.ParkState = individualPark.States;
                    park.ParkDescription = individualPark.Description;
                    park.ParkLatitude = individualPark.Latitude;
                    park.ParkLongitude = individualPark.Longitude;
                    park.ParkCode = individualPark.ParkCode;
                }
                // Check for duplicates
                var uniqueParkCode = _context.Parks.Where(c => c.ParkCode == individualPark.ParkCode).FirstOrDefault();
                if (uniqueParkCode == null)
                {
                    _context.Parks.Add(park);
                    _context.SaveChanges();
                }
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// HIKING TRAIL INFORMATION FROM HIKING DATA PROJECT REST API ENDPOINT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public async Task<string> FetchTrailsApi(int id)
        {
            Park park = await _context.Parks.FindAsync(id);
            var hikingTrailsKey = _configuration["HikingProjectKey"];
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://www.hikingproject.com/data/get-trails?lat={park.ParkLatitude}&lon={park.ParkLongitude}&maxDistance=100&key={hikingTrailsKey}");

            var client = ClientFactory.CreateClient("trails");
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                RestApiHikingProject RestTrails = await response.Content.ReadFromJsonAsync<RestApiHikingProject>();
                List<Trail> trailInfo = RestTrails.trails.ToList();
                await ApplyHikingTrailValues(park, trailInfo);
                await _context.SaveChangesAsync();
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"Status Code: {response.StatusCode}";
            }
        }

        public async Task ApplyHikingTrailValues(Park park, List<Trail> trailInfo)
        {
            foreach (var individualTrail in trailInfo)
            {
                HikingTrail hikingTrail = new HikingTrail
                {
                    TrailName = individualTrail.name,
                    TrailLength = Math.Round(individualTrail.length, 2),
                    TrailDifficulty = individualTrail.difficulty,
                    HikingApiCode = individualTrail.id,
                    ParkId = park.ParkId
                };

                string trailSummary = individualTrail.summary;
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
                string trailCondition = individualTrail.conditionDetails;
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
                // Write conditional to delete trail if park is deleted
                //!!!!!! Currently not adding a few parks because I deleted the original park, but not the associated trails
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
