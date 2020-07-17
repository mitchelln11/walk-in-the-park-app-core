using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace walkinthepark.Models
{
    public class RestApiOpenWeather
    {
        public Coord Coord { get; set; }
        public Weather[] Weather { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
    }

    public class Coord
    {
        public float Lon { get; set; }
        public float Lat { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
    }

    public class Wind
    {
        public float Speed { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
    }
}
