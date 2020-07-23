using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Models;

namespace walkinthepark.Services.Interfaces
{
    public interface IWishlistService
    {
        List<HikerParkWishlist> GetWishlist();
        List<HikerParkWishlist> GetWishlist(int id); // Override

        HikerParkWishlist HikerIdFromWishlist(int id);
        void AddParktoWishlist(int id);
        void DeleteWishlist(int id);
    }
}
