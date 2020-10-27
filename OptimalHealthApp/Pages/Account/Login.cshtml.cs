using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OptimalHealthApp.Hash;
using OptimalHealthApp.Models;

namespace OptimalHealthApp
{
    public class LoginModel : PageModel
    {
        private readonly OptimalHealthApp.Data.OptimalHealthAppContext _context;

        public LoginModel(OptimalHealthApp.Data.OptimalHealthAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string UserTable { get; set;}

        public class InputModel
        {
            [Display(Name = "Login Name")]
            [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string LoginName { get; set; }

            [DataType(DataType.Password)]
            [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
            [Display(Name = "Patient login")]
            public bool IsPatient { get; set; }
            [Display(Name = "Doctor login")]
            public bool IsDoctor { get; set; }
        }
        
        public async Task<IActionResult> OnPostAsync(UserTable usertable, InputModel input)
        {
            HashPassword hash = new HashPassword();
            String hashInputPassword = hash.GenerateSaltedHash(Input.Password);

            if (Input.IsPatient == true && Input.IsDoctor == false)
            {       
                //Checks if hashed password and email are the same as in DB UserTable
                var query1 = from p in _context.UserTable where p.Email == Input.LoginName && p.U_Password == hashInputPassword select p;

                if (query1.Any())
                {       //If DB has user with the same email and password, give claims and log in
                    var claims = new List<Claim>
                    {
                    new Claim("User", Input.LoginName),
                    new Claim("Role", "Patient")
                    };

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "Cookies", "User", "Role");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = Input.RememberMe
                    };

                    await HttpContext.SignInAsync(principal, authProperties);

                    return RedirectToPage("/PatientPages/patientProfilePage");
                }
                else
                {   //or do this
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            if(Input.IsPatient == false && Input.IsDoctor == true)
            {   //checks if password and email are the same as in DB Doctor table
                var query2 = from p in _context.Doctor where p.Email == Input.LoginName && p.D_Password == hashInputPassword select p;

                if (query2.Any())
                {       //If true, gives claims and logs in
                    var claims2 = new List<Claim>
                    {
                    new Claim("User", Input.LoginName),
                    new Claim("Role", "Doctor")
                    };

                    ClaimsIdentity userIdentity2 = new ClaimsIdentity(claims2, "Cookies", "User", "Role");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity2);
                    var authProperties2 = new AuthenticationProperties
                    {
                        IsPersistent = Input.RememberMe
                    };

                    await HttpContext.SignInAsync(principal, authProperties2);

                    return RedirectToPage("/DoctorPages/doctorProfilePage");
                }
                else
                {   //if not true
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            return Page();

        }
    }
}