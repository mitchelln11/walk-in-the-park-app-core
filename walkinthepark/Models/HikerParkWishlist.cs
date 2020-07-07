using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.ViewModels
{
    public class HikerParkWishlist
    {
        // Need to add object AND the unique ID
        public int HikerId { get; set; }
        public Hiker Hiker { get; set; }

        // Need to add object AND the unnique ID
        public int ParkId { get; set; }
        public string ParkName { get; set; }
        public Park Park { get; set; }
    }
}
