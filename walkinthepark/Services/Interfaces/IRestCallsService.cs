using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.Services.Interfaces
{
    public interface IRestCallsService
    {
        Task<string> FetchWeatherApi(Park park); // Park object coming from Details View
        Task<string> FetchTrailsApi(int id);
    }
}
