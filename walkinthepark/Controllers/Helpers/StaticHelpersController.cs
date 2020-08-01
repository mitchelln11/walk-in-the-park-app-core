﻿using System;
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

        //[HttpPost]
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

        //private IEnumerable<SelectListItem> GetStates()
        //{
        //    List<SelectListItem> states = new List<SelectListItem>()
        //    {
        //        new SelectListItem { Text="--Select a State--", Value=""},
        //        new SelectListItem() {Text="Alabama", Value="AL"},
        //        new SelectListItem() { Text="Alaska", Value="AK"},
        //        new SelectListItem() { Text="American Samoa", Value="AS"},
        //        new SelectListItem() { Text="Arizona", Value="AZ"},
        //        new SelectListItem() { Text="Arkansas", Value="AR"},
        //        new SelectListItem() { Text="California", Value="CA"},
        //        new SelectListItem() { Text="Colorado", Value="CO"},
        //        new SelectListItem() { Text="Connecticut", Value="CT"},
        //        new SelectListItem() { Text="District of Columbia", Value="DC"},
        //        new SelectListItem() { Text="Delaware", Value="DE"},
        //        new SelectListItem() { Text="Florida", Value="FL"},
        //        new SelectListItem() { Text="Georgia", Value="GA"},
        //        new SelectListItem() { Text="Hawaii", Value="HI"},
        //        new SelectListItem() { Text="Idaho", Value="ID"},
        //        new SelectListItem() { Text="Illinois", Value="IL"},
        //        new SelectListItem() { Text="Indiana", Value="IN"},
        //        new SelectListItem() { Text="Iowa", Value="IA"},
        //        new SelectListItem() { Text="Kansas", Value="KS"},
        //        new SelectListItem() { Text="Kentucky", Value="KY"},
        //        new SelectListItem() { Text="Louisiana", Value="LA"},
        //        new SelectListItem() { Text="Maine", Value="ME"},
        //        new SelectListItem() { Text="Maryland", Value="MD"},
        //        new SelectListItem() { Text="Massachusetts", Value="MA"},
        //        new SelectListItem() { Text="Michigan", Value="MI"},
        //        new SelectListItem() { Text="Minnesota", Value="MN"},
        //        new SelectListItem() { Text="Mississippi", Value="MS"},
        //        new SelectListItem() { Text="Missouri", Value="MO"},
        //        new SelectListItem() { Text="Montana", Value="MT"},
        //        new SelectListItem() { Text="Nebraska", Value="NE"},
        //        new SelectListItem() { Text="Nevada", Value="NV"},
        //        new SelectListItem() { Text="New Hampshire", Value="NH"},
        //        new SelectListItem() { Text="New Jersey", Value="NJ"},
        //        new SelectListItem() { Text="New Mexico", Value="NM"},
        //        new SelectListItem() { Text="New York", Value="NY"},
        //        new SelectListItem() { Text="North Carolina", Value="NC"},
        //        new SelectListItem() { Text="North Dakota", Value="ND"},
        //        new SelectListItem() { Text="Ohio", Value="OH"},
        //        new SelectListItem() { Text="Oklahoma", Value="OK"},
        //        new SelectListItem() { Text="Oregon", Value="OR"},
        //        new SelectListItem() { Text="Pennsylvania", Value="PA"},
        //        new SelectListItem() { Text="Rhode Island", Value="RI"},
        //        new SelectListItem() { Text="South Carolina", Value="SC"},
        //        new SelectListItem() { Text="South Dakota", Value="SD"},
        //        new SelectListItem() { Text="Tennessee", Value="TN"},
        //        new SelectListItem() { Text="Texas", Value="TX"},
        //        new SelectListItem() { Text="Utah", Value="UT"},
        //        new SelectListItem() { Text="Vermont", Value="VT"},
        //        new SelectListItem() { Text="Virginia", Value="VA"},
        //        new SelectListItem() { Text="Washington", Value="WA"},
        //        new SelectListItem() { Text="West Virginia", Value="WV"},
        //        new SelectListItem() { Text="Wisconsin", Value="WI"},
        //        new SelectListItem() { Text="Wyoming", Value="WY"}
        //    };
        //    return states;
        //}

        readonly List<SelectListItem> states = new List<SelectListItem>()
        {
            new SelectListItem { Text="--Select a State--", Value=""},
            new SelectListItem() {Text="Alabama", Value="AL"},
            new SelectListItem() { Text="Alaska", Value="AK"},
            new SelectListItem() { Text="American Samoa", Value="AS"},
            new SelectListItem() { Text="Arizona", Value="AZ"},
            new SelectListItem() { Text="Arkansas", Value="AR"},
            new SelectListItem() { Text="California", Value="CA"},
            new SelectListItem() { Text="Colorado", Value="CO"},
            new SelectListItem() { Text="Connecticut", Value="CT"},
            new SelectListItem() { Text="District of Columbia", Value="DC"},
            new SelectListItem() { Text="Delaware", Value="DE"},
            new SelectListItem() { Text="Florida", Value="FL"},
            new SelectListItem() { Text="Georgia", Value="GA"},
            new SelectListItem() { Text="Hawaii", Value="HI"},
            new SelectListItem() { Text="Idaho", Value="ID"},
            new SelectListItem() { Text="Illinois", Value="IL"},
            new SelectListItem() { Text="Indiana", Value="IN"},
            new SelectListItem() { Text="Iowa", Value="IA"},
            new SelectListItem() { Text="Kansas", Value="KS"},
            new SelectListItem() { Text="Kentucky", Value="KY"},
            new SelectListItem() { Text="Louisiana", Value="LA"},
            new SelectListItem() { Text="Maine", Value="ME"},
            new SelectListItem() { Text="Maryland", Value="MD"},
            new SelectListItem() { Text="Massachusetts", Value="MA"},
            new SelectListItem() { Text="Michigan", Value="MI"},
            new SelectListItem() { Text="Minnesota", Value="MN"},
            new SelectListItem() { Text="Mississippi", Value="MS"},
            new SelectListItem() { Text="Missouri", Value="MO"},
            new SelectListItem() { Text="Montana", Value="MT"},
            new SelectListItem() { Text="Nebraska", Value="NE"},
            new SelectListItem() { Text="Nevada", Value="NV"},
            new SelectListItem() { Text="New Hampshire", Value="NH"},
            new SelectListItem() { Text="New Jersey", Value="NJ"},
            new SelectListItem() { Text="New Mexico", Value="NM"},
            new SelectListItem() { Text="New York", Value="NY"},
            new SelectListItem() { Text="North Carolina", Value="NC"},
            new SelectListItem() { Text="North Dakota", Value="ND"},
            new SelectListItem() { Text="Ohio", Value="OH"},
            new SelectListItem() { Text="Oklahoma", Value="OK"},
            new SelectListItem() { Text="Oregon", Value="OR"},
            new SelectListItem() { Text="Pennsylvania", Value="PA"},
            new SelectListItem() { Text="Rhode Island", Value="RI"},
            new SelectListItem() { Text="South Carolina", Value="SC"},
            new SelectListItem() { Text="South Dakota", Value="SD"},
            new SelectListItem() { Text="Tennessee", Value="TN"},
            new SelectListItem() { Text="Texas", Value="TX"},
            new SelectListItem() { Text="Utah", Value="UT"},
            new SelectListItem() { Text="Vermont", Value="VT"},
            new SelectListItem() { Text="Virginia", Value="VA"},
            new SelectListItem() { Text="Washington", Value="WA"},
            new SelectListItem() { Text="West Virginia", Value="WV"},
            new SelectListItem() { Text="Wisconsin", Value="WI"},
            new SelectListItem() { Text="Wyoming", Value="WY"}
        };

        //private IEnumerable<string> GetAllStates()
        //{
        //    return new List<string>
        //    {
        //        "AL",
        //        "AK",
        //        "AS",
        //        "AZ",
        //        "AR",
        //        "CA",
        //        "CO",
        //        "CT",
        //        "DE",
        //        "DC",
        //        "FL",
        //        "GA",
        //        "HI",
        //        "ID",
        //        "IL",
        //        "IN",
        //        "IA",
        //        "KS",
        //        "KY",
        //        "LA",
        //        "ME",
        //        "MD",
        //        "MA",
        //        "MI",
        //        "MN",
        //        "MS",
        //        "MO",
        //        "MT",
        //        "NE",
        //        "NV",
        //        "NH",
        //        "NJ",
        //        "NM",
        //        "NY",
        //        "NC",
        //        "ND",
        //        "OH",
        //        "OK",
        //        "OR",
        //        "PA",
        //        "RI",
        //        "SC",
        //        "SD",
        //        "TN",
        //        "TX",
        //        "UT",
        //        "VT",
        //        "VA",
        //        "WA",
        //        "WV",
        //        "WI",
        //        "WY"
        //    };
        //}
    }
}
