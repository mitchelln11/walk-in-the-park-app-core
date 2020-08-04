using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        /// <summary>
        /// ------------ CURRENT USER FROM SignInManager<IdentityUser> signInManager----------
        /// </summary>
        // Find logged in hiker's Application ID
        public string FindRegisteredUserId() => _signInManager.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;



        /// <summary>
        /// ------------ PULL INFO FROM DATABASE----------
        /// </summary>
        public List<Hiker> GetHikers() => _context.Hikers.ToList();

        // Find logged in hiker -- Needed???  Or just use GetHikerId method???
        public Hiker GetLoggedInHiker(int id) => _context.Hikers.Find(id);

        private Hiker GetAppIdFromUsers(string appId) => _context.Hikers.Where(h => h.ApplicationId == appId).FirstOrDefault();
        public void HikerIsRegistered()
        {
            Hiker currentHiker;
            string registrant;

            // Runtime Binding error: Can't bind on null reference (Even when logged in)
            // What to do when it fails? False boolean not possible if Hiker isn't created yet
            try
            {
                registrant = FindRegisteredUserId();
                currentHiker = GetAppIdFromUsers(registrant);
                if (currentHiker.ApplicationId == registrant)
                {
                    currentHiker.IsRegisteredWithProfile = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //_context.Hikers.Update(currentHiker.IsRegisteredWithProfile) = false;
            }
        }

        public Hiker GetLoggedInHikerId()
        {
            var registrant = FindRegisteredUserId();
            return GetAppIdFromUsers(registrant);
        }

        // Find Application User Record - Whole Record
        public IdentityUser CurrentUser(Hiker hiker) => _context.Users.Where(s => s.Id == hiker.ApplicationId).FirstOrDefault();

        // Look for specific hiker
        public Hiker GetHikerRecord(int? id) => _context.Hikers.Where(i => i.HikerId == id).FirstOrDefault(); // not working

        public int GetHikerId(int id)
        {
            Hiker hikerId = _context.Hikers.Where(i => i.HikerId == id).FirstOrDefault();
            return hikerId.HikerId;
        }

        public string GetHikerFirstName()
        {
            string appId = FindRegisteredUserId();
            Hiker hikerRecord = _context.Hikers.Where(i => i.ApplicationId == appId).FirstOrDefault();
            return hikerRecord.FirstName;
        }

        public string GetHikerFirstName(string firstName) // Overload for first name
        {
            Hiker hikerFirstName = _context.Hikers.Where(i => i.FirstName == firstName).FirstOrDefault();
            return hikerFirstName.FirstName;
        }

        public string GetHikerLastName(string lastName)
        {
            Hiker hikerLastName = _context.Hikers.Where(i => i.LastName == lastName).FirstOrDefault();
            return hikerLastName.LastName;
        }

        public string GetHikerAddress(string address)
        {
            Hiker hikerAddress = _context.Hikers.Where(i => i.StreetAddress == address).FirstOrDefault();
            return hikerAddress.StreetAddress;
        }

        public string GetHikerCity(string city)
        {
            Hiker hikerCity = _context.Hikers.Where(i => i.City == city).FirstOrDefault();
            return hikerCity.City;
        }

        public string GetHikerState(string state)
        {
            Hiker hikerState = _context.Hikers.Where(i => i.SelectedState == state).FirstOrDefault();
            return hikerState.SelectedState;
        }

        public string GetHikerLatitude(string latitude)
        {
            Hiker hikerLatitude = _context.Hikers.Where(i => i.Latitude == latitude).FirstOrDefault();
            return hikerLatitude.Latitude;
        }

        public string GetHikerLongitude(string longitude)
        {
            Hiker hikerLongitude = _context.Hikers.Where(i => i.Latitude == longitude).FirstOrDefault();
            return hikerLongitude.Longitude;
        }

        /// <summary>
        /// ------------ TESTING ----------
        /// </summary>
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



        /// <summary>
        /// ------------ DATABASE MANIPULATION ----------
        /// </summary>
        /// 
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

        public void DeleteApplicant(string appId)
        {
            try
            {
                IdentityUser user = _context.Users.Where(h => h.Id == appId).FirstOrDefault();
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
