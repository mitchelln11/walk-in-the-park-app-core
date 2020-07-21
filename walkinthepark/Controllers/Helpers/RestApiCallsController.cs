using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Controllers.Helpers
{
    public class RestApiCallsController : Controller
    {
        private readonly IRestCallsService _restCalls;

        public RestApiCallsController(IRestCallsService restCalls)
        {
            _restCalls = restCalls;
        }

        public async Task<RedirectToActionResult> FetchParksRestHelper()
        {
            await _restCalls.FetchParksApi();
            return RedirectToAction("Index", "Park");
        }
    }
}
