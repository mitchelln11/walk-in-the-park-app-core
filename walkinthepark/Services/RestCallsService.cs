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
        public bool GetError { get; private set; }

        public RestCallsService(ApplicationDbContext context,  IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _context = context;
            ClientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<string> FetchWeatherApi(Park park)
        {
            //var parkId = _parkService.GetParkId();
            var weatherKey = _configuration["OpenWeatherKey"];
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.openweathermap.org/data/2.5/weather?lat={park.ParkLatitude}&lon={park.ParkLongitude}&APPID={weatherKey}");

            var client = ClientFactory.CreateClient("weather");
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Try updating the Currentweather Database Info here

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
    }
}
