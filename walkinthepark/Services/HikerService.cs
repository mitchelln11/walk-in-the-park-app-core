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
        public bool RegisteredAndHasProfile = false;
        public string firstName;

        public HikerService(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public List<Hiker> GetHikers() => _context.Hikers.ToList();

        // Find logged in hiker's Application ID
        public string FindRegisteredUserId() => _signInManager.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        // Find logged in hiker -- Needed???  Or just use GetHikerId method???
        public Hiker GetLoggedInHikerId(int id) => _context.Hikers.Find(id);

        // Find Application User Record - Whole Record
        public IdentityUser CurrentUser(Hiker hiker) => _context.Users.Where(s => s.Id == hiker.ApplicationId).FirstOrDefault();

        // Look for specific hiker
        public Hiker GetHikerRecord(int id) => _context.Hikers.Where(i => i.HikerId == id).FirstOrDefault(); // not working

        public int GetHikerId(int id)
        {
            var hikerId = _context.Hikers.Where(i => i.HikerId == id).FirstOrDefault();
            return hikerId.HikerId;
        }

        public string GetHikerFirstName()
        {
            string appId = FindRegisteredUserId();
            Hiker hikerRecord = _context.Hikers.Where(i => i.ApplicationId == appId).FirstOrDefault();
            firstName = hikerRecord.FirstName;
            return firstName;
        }

        public string GetHikerFirstName(string firstName) // Overload for first name
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

        // For Navbar conditional
        public bool HikerRegisteredProfileBuilt()
        {
            bool RegisteredAndHasProfile = false;
            var registeredHiker = FindRegisteredUserId();
            var hikerProfileCreated = _context.Hikers.Any(h => h.ApplicationId == registeredHiker);
            if ((registeredHiker != null) && (hikerProfileCreated == true))
            {
                return RegisteredAndHasProfile = true;
            }
            return RegisteredAndHasProfile;
        }

        public void AddHiker(Hiker hiker)
        {
            try
            {
                _context.Hikers.Add(hiker);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditHiker(Hiker hiker)
        {
            try
            {
                _context.Hikers.Update(hiker);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteHiker(int id)
        {
            try
            {
                Hiker hiker = _context.Hikers.Where(h => h.HikerId == id).FirstOrDefault();
                _context.Hikers.Remove(hiker);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
