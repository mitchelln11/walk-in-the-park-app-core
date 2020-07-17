using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walkinthepark.Data;
using walkinthepark.Models;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Services
{
    public class HikingTrailService : IHikingTrailService
    {
        private readonly ApplicationDbContext _context;

        public HikingTrailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<HikingTrail> GetTrails() => _context.HikingTrails.ToList();

        // Look for specific trail
        public HikingTrail GetTrailRecord(int id)
        {
            var trailRecord = _context.HikingTrails.Where(i => i.ParkId == id).FirstOrDefault();
            return trailRecord;
        }

        public int GetTrailId(int id)
        {
            var trailId = _context.HikingTrails.Where(s => s.TrailId == id).FirstOrDefault();
            return trailId.TrailId;
        }

        public string GetTrailName(string trailName)
        {
            var trailId = _context.HikingTrails.Where(s => s.TrailName == trailName).FirstOrDefault();
            return trailId.TrailName;
        }

        public string GetTrailDifficulty(string difficulty)
        {
            var trailDifficulty = _context.HikingTrails.Where(s => s.TrailDifficulty == difficulty).FirstOrDefault();
            return trailDifficulty.TrailDifficulty;
        }

        public string GetTrailSummary(string summary)
        {
            var trailSummary = _context.HikingTrails.Where(s => s.TrailSummary == summary).FirstOrDefault();
            return trailSummary.TrailSummary;
        }

        public double GetTrailLength(double length)
        {
            var trailLength = _context.HikingTrails.Where(s => s.TrailLength == length).FirstOrDefault();
            return trailLength.TrailLength;
        }

        public string GetTrailCondition(string condition)
        {
            var trailCondition = _context.HikingTrails.Where(s => s.TrailCondition == condition).FirstOrDefault();
            return trailCondition.TrailCondition;
        }

        public int GetTrailCode(int code)
        {
            var trailCode = _context.HikingTrails.Where(s => s.HikingApiCode == code).FirstOrDefault();
            return trailCode.HikingApiCode;
        }
    }
}
