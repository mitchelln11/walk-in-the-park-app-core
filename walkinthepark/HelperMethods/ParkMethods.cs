using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.HelperMethods
{
    public class ParkMethods
    {
        public async Task GetParkLatLong(Park park)
        {
            var comboLatLong = park.ComboParkLatLng.Split().ToArray(); // How data comes in from API, must be split
            //var latLongArray = comboLatLong.Split().ToArray(); // Split on comma, return array
            string isolatedLatitude = comboLatLong[0].TrimEnd(','); // Latitude value
            string isolatedLongitude = comboLatLong[1].TrimEnd(','); // Longitude value
            string latitude = isolatedLatitude.Substring(4, isolatedLatitude.Length - 4);
            string longitude = isolatedLongitude.Substring(5, isolatedLongitude.Length - 5);
            park.ParkLatitude = latitude;
            park.ParkLongitude = longitude;
        }
    }
}
