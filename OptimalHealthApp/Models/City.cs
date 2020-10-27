using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OptimalHealthApp.Models
{
    public class City
    {
        [Key]
        public int City_nr { get; set; }

        public string City_name { get; set; }

        public int County_ID { get; set; }
        public County County { get; set; }
        public List<Health_center> Health_Centers { get; set; }
    }
}
