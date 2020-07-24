using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace walkinthepark.Models
{

    public class RestApiHikingProject
    {
        public Trail[] Trails { get; set; }
        public int Success { get; set; }
    }

    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Difficulty { get; set; }
        public float Length { get; set; }
        public string ConditionDetails { get; set; }
    }

}
