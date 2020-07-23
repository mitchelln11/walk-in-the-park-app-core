using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.Services.Interfaces
{
    public interface IHikerService
    {
        Hiker GetHikerRecord(int id);
        List<Hiker> GetHikers();
        int GetHikerId(int id);
        string FindRegisteredUserId();
        string GetHikerFirstName();
        bool HikerRegisteredProfileBuilt();
        void AddHiker(Hiker hiker);
        void EditHiker(Hiker hiker);
        void DeleteHiker(int id);
        void DeleteApplicant(string appId); // AspNetUsers that gets attached to the Hiker
    }
}
