using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.ViewModels
{
    public class StaticDataViewModel
    {
        public List<Park> Park { get; set; }
        public List<SelectListItem> States { get; set; }

        public Park ParkObj { get; set; }
        public List<Park> FilteredParkList { get; set; }
    }
}
