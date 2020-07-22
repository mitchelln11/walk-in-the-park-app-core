using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using walkinthepark.Models;
using System.Net.Http;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IHikerService _hikerService;

        //public HomeController(IHikerService hikerService)
        //{
        //    _hikerService = hikerService;
        //}
        public IActionResult Index() 
        {
            //var name = _hikerService.GetHikerFirstName();
            //var registeredProfiled = _hikerService.HikerRegisteredProfileBuilt();
            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}
