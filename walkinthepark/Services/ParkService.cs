using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using walkinthepark.Data;
using walkinthepark.Models;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Services
{
    public class ParkService : IParkService
    {
        private readonly ApplicationDbContext _context;

        public ParkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Park> GetParks() => _context.Parks.ToList();

        public Park GetParkRecord(int id) => _context.Parks.Where(i => i.ParkId == id).FirstOrDefault();

        public void DeletePark(int id)
        {
            try
            {
                Park park = _context.Parks.Where(t => t.ParkId == id).FirstOrDefault();
                _context.Parks.Remove(park);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// TESTING ---------------------------------------
        /// </summary>

        ///
        /// END TESTING -----------------------------------
        ///





        public int GetParkId(int id)
        {
            var parkId = _context.Parks.Where(i => i.ParkId == id).FirstOrDefault();
            return parkId.ParkId;
        }

        public string GetParkName(string name)
        {
            var parkName = _context.Parks.Where(i => i.ParkName == name).FirstOrDefault();
            return parkName.ParkName;
        }

        public string GetParkState(string state)
        {
            var parkState = _context.Parks.Where(s => s.ParkState == state).FirstOrDefault();
            return parkState.ParkState;
        }

        public string GetParkLatitude(string latitude)
        {
            var parkLatitude = _context.Parks.Where(s => s.ParkLatitude == latitude).FirstOrDefault();
            return parkLatitude.ParkLatitude;
        }

        public string GetParkLongitude(string longitude)
        {
            var parkLongitude = _context.Parks.Where(s => s.ParkLongitude == longitude).FirstOrDefault();
            return parkLongitude.ParkLongitude;
        }

        public string GetParkDescription(string description)
        {
            var parkDescription = _context.Parks.Where(s => s.ParkDescription == description).FirstOrDefault();
            return parkDescription.ParkDescription;
        }

        public string GetParkCode(string code)
        {
            var parkCode = _context.Parks.Where(s => s.ParkCode == code).FirstOrDefault();
            return parkCode.ParkCode;
        }
    }
}
