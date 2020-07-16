using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.Services.Interfaces
{
    public interface IHikingTrailService
    {
        List<HikingTrail> GetTrails();
    }
}
