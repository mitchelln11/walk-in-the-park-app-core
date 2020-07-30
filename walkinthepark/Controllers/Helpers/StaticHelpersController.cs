using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using walkinthepark.Models;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Controllers.Helpers
{
    public class StaticHelpersController : Controller
    {
        /// <summary>
        /// -----------------STATE DROPDOWNS-----------------------------
        /// </summary>
        //public ActionResult GetStateFromList()
        //{
        //    var states = GetAllStates(); // List of all states in dropdown

        //    var hiker = new Hiker
        //    {
        //        StatesList = GetSelectListItems(states) // needs to pass a List
        //    };

        //    return View(hiker);
        //}

        [HttpPost]
        //public ActionResult GetStateFromList(Hiker Hiker)
        //{
        //    var states = GetAllStates(); // In order to show change value
        //    Hiker.StatesList = GetSelectListItems(states);

        //    if(ModelState.IsValid)
        //    {
        //        Session["Hiker"] = Hiker;
        //        return RedirectToAction()
        //    }
        //}

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }
            return selectList;
        }

        private IEnumerable<string> GetAllStates()
        {
            return new List<string>
            {
                "AL",
                "AK",
                "AS",
                "AZ",
                "AR",
                "CA",
                "CO",
                "CT",
                "DE",
                "DC",
                "FL",
                "GA",
                "HI",
                "ID",
                "IL",
                "IN",
                "IA",
                "KS",
                "KY",
                "LA",
                "ME",
                "MD",
                "MA",
                "MI",
                "MN",
                "MS",
                "MO",
                "MT",
                "NE",
                "NV",
                "NH",
                "NJ",
                "NM",
                "NY",
                "NC",
                "ND",
                "OH",
                "OK",
                "OR",
                "PA",
                "RI",
                "SC",
                "SD",
                "TN",
                "TX",
                "UT",
                "VT",
                "VA",
                "WA",
                "WV",
                "WI",
                "WY"
            };
        }
    }
}
