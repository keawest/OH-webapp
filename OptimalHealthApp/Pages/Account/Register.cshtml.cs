using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using OptimalHealthApp.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using OptimalHealthApp.Hash;

namespace OptimalHealthApp
{
    public class RegisterModel : PageModel
    {
        private readonly OptimalHealthApp.Data.OptimalHealthAppContext _context;

        public RegisterModel(OptimalHealthApp.Data.OptimalHealthAppContext context)
        {
            _context = context;
        }
        [BindProperty]
        public UserTable UserTable { get; set; }
        [BindProperty]
        public List<SelectListItem> CountyOptions { get; set; }
        [BindProperty]
        public List<SelectListItem> CityOptions { get; set; }
        public void OnGet()
        {
            CountyOptions = _context.County.Select(a =>
                              new SelectListItem
                              {
                                  Value = a.County_ID.ToString(),
                                  Text = a.County_name
                              }).ToList();

            CityOptions = _context.City.Select(a =>
                               new SelectListItem
                               {
                                   Value = a.City_nr.ToString(),
                                   Text = a.City_name
                               }).ToList();
        }
        public async Task<IActionResult> OnPostAsync(UserTable emptyUsertable)
        {
            HashPassword hash = new HashPassword();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (await TryUpdateModelAsync<UserTable>(
                 emptyUsertable,
                 "UserTable",
                 s => s.County, s => s.City, s => s.Adress, s => s.Email, s => s.f_name, s =>s.l_name, s =>s.Nationality, s =>s.RGP, s =>s.Telephone_1, s =>s.Telephone_2, s =>s.U_Password, s =>s.Zipcode, s =>s.Rolle))
            {
                HashPassword hashPassword = new HashPassword();
                emptyUsertable.U_Password = hashPassword.GenerateSaltedHash(emptyUsertable.U_Password);
                if (UserTable.Email == emptyUsertable.Email) //checks if the email address already exists in DB
                {
                    return NotFound();
                }
                else
                {
                    _context.UserTable.Add(emptyUsertable);
                    await _context.SaveChangesAsync();
                }
            }
           return RedirectToPage("/Index");
        }
    }
}
