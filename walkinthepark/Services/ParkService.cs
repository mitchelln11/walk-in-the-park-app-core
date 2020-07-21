using Microsoft.AspNetCore.Mvc;
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
    public class ParkService : IParkService
    {
        private readonly ApplicationDbContext _context;

        public ParkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Park> GetParks() => _context.Parks.ToList();

        public Park GetParkRecord(int id) => _context.Parks.Where(i => i.ParkId == id).FirstOrDefault();

        /// <summary>
        /// TESTING ---------------------------------------
        /// </summary>
        public void DeletePark(int id)
        {
            try
            {
                Park park = _context.Parks.Where(t => t.ParkId == id).FirstOrDefault();
                _context.Parks.Remove(park);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        ///
        /// END TESTING -----------------------------------
        ///





        public int GetParkId(int id)
        {
            var parkId = _context.Parks.Where(i => i.ParkId == id).FirstOrDefault();
            return parkId.ParkId;
        }

        public string GetParkName(string name)
        {
            var parkName = _context.Parks.Where(i => i.ParkName == name).FirstOrDefault();
            return parkName.ParkName;
        }

        public string GetParkState(string state)
        {
            var parkState = _context.Parks.Where(s => s.ParkState == state).FirstOrDefault();
            return parkState.ParkState;
        }

        public string GetParkLatitude(string latitude)
        {
            var parkLatitude = _context.Parks.Where(s => s.ParkLatitude == latitude).FirstOrDefault();
            return parkLatitude.ParkLatitude;
        }

        public string GetParkLongitude(string longitude)
        {
            var parkLongitude = _context.Parks.Where(s => s.ParkLongitude == longitude).FirstOrDefault();
            return parkLongitude.ParkLongitude;
        }

        public string GetParkDescription(string description)
        {
            var parkDescription = _context.Parks.Where(s => s.ParkDescription == description).FirstOrDefault();
            return parkDescription.ParkDescription;
        }

        public string GetParkCode(string code)
        {
            var parkCode = _context.Parks.Where(s => s.ParkCode == code).FirstOrDefault();
            return parkCode.ParkCode;
        }

        //public async Task FetchParkApi()
        //{
        //    _ = _configuration["NpsKey"];
        //    var request = new HttpRequestMessage(HttpMethod.Get,
        //        "https://developer.nps.gov/api/v1/parks?q=National%20Park&limit=91&api_key={parkKey}");

        //    var client = _clientFactory.CreateClient();

        //    var response = await client.SendAsync(request);

        //    if(response.IsSuccessStatusCode)
        //    {
        //        using var responseStream = await response.Content.ReadAsStreamAsync();
        //        Parks = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<Park>>(responseStream);
        //    }
        //    else
        //    {
        //        GetParkError = true;
        //        Parks = Array.Empty<Park>();
        //    }
        //}


        ////////---------------- WEATHER --------------------/////////////////
        //public async Task<ActionResult> FetchWeatherApi(Park park) // Referenced on Button click
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
        //    return ActionResult("Details", "Parks", park.ParkId);
        //}

        //public async Task<SearchDataResponse> SearchAsync(string query, string accessToken)
        //{
        //    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri)
        //    {
        //        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.SendAsync(request);
        //    }
        //}

        //public async Task FetchWeatherApi(Park park)
        //{
        //    var weatherKey = _configuration["OpenWeatherKey"];
        //    var client = _clientFactory.CreateClient("weather");

        //    try
        //    {
        //        restWeather = await client.GetFromJsonAsync<RestApiOpenWeather>($"?lat={park.ParkLatitude}&lon={park.ParkLongitude}&APPID={weatherKey}");
        //        park.CurrentWeatherInfo.Temperature = GetCurrentTemperature(restWeather.main.temp);
        //        park.CurrentWeatherInfo.Condition = restWeather.weather[0].main;
        //        park.CurrentWeatherInfo.Wind = Math.Round(restWeather.wind.speed, 2);
        //        await _context.SaveChangesAsync();
        //        errorString = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorString = $"There was an error with the REST API: { ex.Message }";
        //    }
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
    }
}
