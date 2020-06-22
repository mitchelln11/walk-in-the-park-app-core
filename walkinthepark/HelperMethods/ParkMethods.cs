using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.HelperMethods
{
    public class ParkMethods : Controller
    {
        //public async Task GetParkLatLong(Park park)
        //{
        //    var comboLatLong = park.ComboParkLatLng.Split().ToArray(); // How data comes in from API, must be split
        //    //var latLongArray = comboLatLong.Split().ToArray(); // Split on comma, return array
        //    string isolatedLatitude = comboLatLong[0].TrimEnd(','); // Latitude value
        //    string isolatedLongitude = comboLatLong[1].TrimEnd(','); // Longitude value
        //    string latitude = isolatedLatitude.Substring(4, isolatedLatitude.Length - 4);
        //    string longitude = isolatedLongitude.Substring(5, isolatedLongitude.Length - 5);
        //    park.ParkLatitude = latitude;
        //    park.ParkLongitude = longitude;
        //}

        public async Task FetchParkApi()
        {
            string url = $"https://developer.nps.gov/api/v1/parks?q=National%20Park&limit=91&api_key=l1TfNkPWDHe8qoWyjkjznG1yil0Gij63mSNZVrIr";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonresult = await response.Content.ReadAsStringAsync();
            Park park = new Park(); // Instantiate a new park
            if (response.IsSuccessStatusCode)
            {
                RestApiNationalParks parkInfo = JsonConvert.DeserializeObject<RestApiNationalParks>(jsonresult); // Run through entire JSON file
                var parkList = parkInfo.data.Select(m => m).ToList(); // returns entire list of Parks, up to 150, this only has 90 based of the National Parks query
                Console.WriteLine(parkList);
                //await _context.SaveChangesAsync();
            }
        }
    }
}
