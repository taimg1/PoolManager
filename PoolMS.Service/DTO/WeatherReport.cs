using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Service.DTO
{
    public class WeatherReport()
    {
        public DateTime Date { get; set; }
        public double LowTemperature { get; set; }
        public double HighTemperature { get; set; }
        public string Summary { get; set; }
        public double WindSpeed { get; set; }   

    }
}
