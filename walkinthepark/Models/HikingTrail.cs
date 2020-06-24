using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace walkinthepark.Models
{
    public class HikingTrail
    {
        [Key]
        public int TrailId { get; set; }

        [Display(Name = "Trail Name")]
        public string TrailName { get; set; }

        [Display(Name = "Difficulty")]
        public string TrailDifficulty { get; set; }

        [Display(Name = "Summary")]
        public string TrailSummary { get; set; }

        [Display(Name = "Length")]
        public double TrailLength { get; set; }

        [Display(Name = "Trail Condition")]
        public string TrailCondition { get; set; }

        public int HikingApiCode { get; set; } // Unique code to prevent duplicates

        [ForeignKey("Park")]
        public int ParkId { get; set; }
        public Park Park { get; set; }
    }
}
