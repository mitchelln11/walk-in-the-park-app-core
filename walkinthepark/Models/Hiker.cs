using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Models
{
    public class Hiker
    {
        [Key]
        public int HikerId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name ="State")]
        public string SelectedState { get; set; }
        
        [NotMapped]
        public List<SelectListItem> States { get; set; }

        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Display(Name = "Longitude")]
        public string Longitude { get; set; }
        public bool EmptyWishlist { get; set; }

        [ForeignKey("ApplicationUser")]
        [HiddenInput(DisplayValue = false)]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsRegisteredWithProfile { get; set; } = false;
        public List<HikerParkWishlist> Wishlists { get; set; }
    }
}
