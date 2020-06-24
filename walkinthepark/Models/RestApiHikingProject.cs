using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace walkinthepark.Models
{

    public class RestApiHikingProject
    {
        public Trail[] trails { get; set; }
        public int success { get; set; }
    }

    public class Trail
    {
        public int id { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string difficulty { get; set; }
        public float length { get; set; }
        public string conditionDetails { get; set; }
    }

}
