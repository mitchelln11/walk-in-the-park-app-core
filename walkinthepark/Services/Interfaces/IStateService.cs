using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace walkinthepark.Services.Interfaces
{
    public interface IStateService
    {
        List<SelectListItem> GetStates();
    }
}
