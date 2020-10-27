using System.ComponentModel.DataAnnotations;

namespace OptimalHealthApp.Models
{
    public class UserTable
    {
        [Key]
        public int U_ID { get; set; }

        [Display(Name = "First name")]
        public string f_name { get; set; }

        [Display(Name = "Last name")]
        public string l_name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        public string U_Password { get; set; }

        [Display(Name = "Telephone")]
        [StringLength(8, ErrorMessage = "The number must be 8 characters long", MinimumLength = 8)]
        public string Telephone_1 { get; set; }
        [StringLength(8, ErrorMessage = "The number must be 8 characters long", MinimumLength = 8)]
        public string Telephone_2 { get; set; }

        public string Adress { get; set; }

        public string Zipcode { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Nationality { get; set; }
        public int RGP { get; set; }
        public string Rolle { get; set; }
    }
}
