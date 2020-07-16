using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using walkinthepark.Data;
using walkinthepark.Models;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Services
{
    public class HikerService : IHikerService
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager; // To find out if they're logged in

        public HikerService(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public List<Hiker> GetHikers()
        {
            var hikers = _context.Hikers.ToList();
            return hikers;
        }

        // Find logged in hiker's Application ID
        public string FindRegisteredUserId() => _signInManager.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        // Find logged in hiker -- Needed???  Or just use GetHikerId method???
        public Hiker GetLoggedInHikerId(int id) => _context.Hikers.Find(id);

        // Find Application User Record - Whole Record
        public IdentityUser CurrentUser(Hiker hiker) => _context.Users.Where(s => s.Id == hiker.ApplicationId).FirstOrDefault();

        // Look for specific hiker
        public Hiker GetHikerRecord(int id) => _context.Hikers.Where(i => i.HikerId == id).FirstOrDefault();

        public int GetHikerId(int id)
        {
            var hikerId = _context.Hikers.Where(i => i.HikerId == id).FirstOrDefault();
            return hikerId.HikerId;
        }

        public string GetHikerFirstName(string firstName)
        {
            var hikerFirstName = _context.Hikers.Where(i => i.FirstName == firstName).FirstOrDefault();
            return hikerFirstName.FirstName;
        }

        public string GetHikerLastName(string lastName)
        {
            var hikerLastName = _context.Hikers.Where(i => i.LastName == lastName).FirstOrDefault();
            return hikerLastName.LastName;
        }

        public string GetHikerAddress(string address)
        {
            var hikerAddress = _context.Hikers.Where(i => i.StreetAddress == address).FirstOrDefault();
            return hikerAddress.StreetAddress;
        }

        public string GetHikerCity(string city)
        {
            var hikerCity = _context.Hikers.Where(i => i.City == city).FirstOrDefault();
            return hikerCity.City;
        }

        public string GetHikerState(string state)
        {
            var hikerState = _context.Hikers.Where(i => i.State == state).FirstOrDefault();
            return hikerState.State;
        }

        public string GetHikerLatitude(string latitude)
        {
            var hikerLatitude = _context.Hikers.Where(i => i.Latitude == latitude).FirstOrDefault();
            return hikerLatitude.Latitude;
        }

        public string GetHikerLongitude(string longitude)
        {
            var hikerLongitude = _context.Hikers.Where(i => i.Latitude == longitude).FirstOrDefault();
            return hikerLongitude.Longitude;
        }
    }
}
