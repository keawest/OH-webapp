using System.ComponentModel.DataAnnotations;

namespace OptimalHealthApp.Models
{
    public class Doctor
    {
        [Key]

        public int D_ID { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int H_ID { get; set; }
        public string D_Password { get; set; }
    }
}
