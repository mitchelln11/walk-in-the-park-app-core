using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.Services.Interfaces
{
    public interface IHikerService
    {
        // Data retrieval
        Hiker GetHikerRecord(int? id);
        List<Hiker> GetHikers();
        Hiker GetLoggedInHikerId();
        int GetHikerId(int id);
        string FindRegisteredUserId();
        string GetHikerFirstName();
        bool HikerRegisteredProfileBuilt();
        IdentityUser CurrentUser(Hiker hiker);
        List<SelectListItem> AssignStateList();


        // Data manipulation
        void AddHiker(Hiker hiker);
        void EditHiker(Hiker hiker);
        void DeleteHiker(int id);
        void DeleteApplicant(string appId); // AspNetUsers that gets attached to the Hiker
    }
}
