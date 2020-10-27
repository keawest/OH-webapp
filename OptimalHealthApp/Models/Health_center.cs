using System.ComponentModel.DataAnnotations;

namespace OptimalHealthApp.Models
{
    public class Health_center
    {

        [Key]
        public int H_ID { get; set; }
        public string H_name { get; set; }
        public int City_nr { get; set; }
       
        public City City { get; set; }

    }
}
