using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace walkinthepark.Models
{
    public class ParkFilterViewModel
    {
        public List<Park> Parks { get; set; } // Access to match against parks in the park table

        [Display(Name = "Select State")]
        public string SelectedState { get; set; } // To hold the value of the selected dropdown state

        public SelectList States { get; set; } // SelectList is unique to make a dropdown, not the name of the object

    }
}
