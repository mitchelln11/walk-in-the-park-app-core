using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        /// <summary>
        /// ------------ PULL INFO FROM DATABASE ----------
        /// </summary>
        // Find logged in hiker's Application ID
        public List<Park> GetParks() => _context.Parks.OrderBy(p => p.ParkName).ToList();
        public List<SelectListItem> GetStatesWithParks()
        {
            List<string> states = GetParks().Select(s => s.ParkState).Distinct().ToList(); // Run Method to get all States from Park records and avoid duplicates with Distinct
            return FilterParksWithMultipeStates(states).Select(m => new SelectListItem { Text = m, Value = m }).ToList(); // Filter multi-state parks
            //var listToItemConversion = multi
            //return multiStateFiltering;
        }

        private List<string> FilterParksWithMultipeStates(List<string> statesWithParks)
        { 
            List<string> finalizedStateList = new List<string>(); // New list to add final results to
            List<string> tempStateList = new List<string>();

            foreach (var state in statesWithParks)
            {
                if (!state.Contains(","))
                {
                    finalizedStateList.Add(state);  // If not in multiple states, add to the finalized list
                }
                else
                {
                    tempStateList.Add(state); // Add state to temp list for filtering
                }
            }

            /// <summary>
            /// ------------ FOR MULTI-STATE PARKS, separate parks by comma, then add each array index to final list, then reorder and avoid duplicates ----------
            /// </summary>
            for (var i = 0; i < tempStateList.Count; i++)
            {
                string[] stringArray = tempStateList[i].Split(',');
                foreach (var stateArray in stringArray)
                {
                    finalizedStateList.Add(stateArray);
                }
            }
            List<string> reorderedFinalList = finalizedStateList.Distinct().OrderBy(s => s).ToList();
            return reorderedFinalList;
        }

        public Park GetParkRecord(int id) => _context.Parks.Where(i => i.ParkId == id).FirstOrDefault();

        public int GetParkId(int id)
        {
            Park parkId = _context.Parks.Where(i => i.ParkId == id).FirstOrDefault();
            return parkId.ParkId;
        }

        public string GetParkName(string name)
        {
            Park parkName = _context.Parks.Where(i => i.ParkName == name).FirstOrDefault();
            return parkName.ParkName;
        }

        public string GetParkState(string state)
        {
            Park parkState = _context.Parks.Where(s => s.ParkState == state).FirstOrDefault();
            return parkState.ParkState;
        }

        public string GetParkLatitude(string latitude)
        {
            Park parkLatitude = _context.Parks.Where(s => s.ParkLatitude == latitude).FirstOrDefault();
            return parkLatitude.ParkLatitude;
        }

        public string GetParkLongitude(string longitude)
        {
            Park parkLongitude = _context.Parks.Where(s => s.ParkLongitude == longitude).FirstOrDefault();
            return parkLongitude.ParkLongitude;
        }

        public string GetParkDescription(string description)
        {
            Park parkDescription = _context.Parks.Where(s => s.ParkDescription == description).FirstOrDefault();
            return parkDescription.ParkDescription;
        }

        public string GetParkCode(string code)
        {
            Park parkCode = _context.Parks.Where(s => s.ParkCode == code).FirstOrDefault();
            return parkCode.ParkCode;
        }

        /// <summary>
        /// ------------ Order Database Information ----------
        /// </summary>
        
        public List<Park> ParkOrderByState() => _context.Parks.OrderBy(s => s.ParkState).ToList();
        public List<Park> ParkReverseOrderByState() => _context.Parks.OrderByDescending(s => s.ParkState).ToList();



        /// <summary>
        /// ------------ DATABASE MANIPULATION ----------
        /// </summary>

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
    }
}
