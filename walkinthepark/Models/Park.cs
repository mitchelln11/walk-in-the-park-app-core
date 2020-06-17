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
        public string ComboParkLatLng { get; set; }

        [NotMapped]
        public string Designation { get; set; }
    }
}
