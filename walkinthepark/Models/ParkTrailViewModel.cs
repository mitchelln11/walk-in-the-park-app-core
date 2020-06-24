using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace walkinthepark.Models
{
    public class ParkTrailViewModel
    {
        [Key]
        public int ParkTrailId { get; set; }

        [ForeignKey("Park")]
        public int ParkId { get; set; }
        public CurrentWeatherInfo CurrentWeatherInfo { get; set; }
        public Park Park { get; set; }

        [ForeignKey("HikingTrail")]
        public int TrailId { get; set; }

        public HikingTrail HikingTrail { get; set; }
        public List<HikingTrail> trails { get; set; }
    }
}
