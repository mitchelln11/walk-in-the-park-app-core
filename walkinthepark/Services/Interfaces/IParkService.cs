using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.Services.Interfaces
{
    public interface IParkService 
    {
        List<Park> GetParks();
        List<SelectListItem> GetStatesWithParks();
        Park GetParkRecord(int id);
        int GetParkId(int id);
        void DeletePark(int id);
    }
}
