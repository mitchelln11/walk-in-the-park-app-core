using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace walkinthepark.Models
{
    public class Park
    {
        [Key]
        public int ParkId { get; set; }

        [Display(Name = "Park Name")]
        public string ParkName { get; set; }

        [Display(Name = "State")]
        public string ParkState { get; set; }

        [Display(Name = "Latitude")]
        public string ParkLatitude { get; set; }

        [Display(Name = "Longitude")]
        public string ParkLongitude { get; set; }

        [Display(Name = "Description")]
        public string ParkDescription { get; set; }

        [Display(Name = "Park Code")]
        public string ParkCode { get; set; }

        [NotMapped]
        public string Designation { get; set; }

        // Weather
        [NotMapped]
        public CurrentWeatherInfo CurrentWeatherInfo { get; set; } // From below

        // Google Maps Markers
        [NotMapped] 
        public ParkMarkers ParkMarkers { get; set; }  // From below

        [NotMapped]
        public List<HikingTrail> HikingTrail  { get; set; }

        public List<HikerParkWishlist> Wishlists { get; set; }
    }

    public class CurrentWeatherInfo
    {
        public double Temperature { get; set; }
        public double Wind { get; set; }
        public string Condition { get; set; }
    }

    public class ParkMarkers
    {
        //public string ParkUniqueCode { get; set; }
        public string ParkLatitude { get; set; }
        public string ParkLongitude { get; set; }
    }
}
