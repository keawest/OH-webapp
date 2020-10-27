using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OptimalHealthApp.Models
{
    public class County
    {
        [Key]
        public int County_ID { get; set;}
        public string County_name { get; set; }
        public List<City> Cities { get; set; }
    }
}
