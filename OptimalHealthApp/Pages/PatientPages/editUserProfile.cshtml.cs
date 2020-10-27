using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptimalHealthApp.Models;

namespace OptimalHealthApp
{
    [Authorize(Roles = "Patient")]
    public class EditUserProfile : PageModel
    {
        private readonly OptimalHealthApp.Data.OptimalHealthAppContext _context;

        public EditUserProfile(OptimalHealthApp.Data.OptimalHealthAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserTable UserTable { get; set; }
        [BindProperty]
        public List<SelectListItem> CountyOptions { get; set; }
        [BindProperty]
        public List<SelectListItem> CityOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int ? id)
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
                       Value = a.City_name.ToString(),
                       Text = a.City_name
                   }).ToList();

            UserTable = await _context.UserTable.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);

            if (UserTable == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(UserTable updateTable)
        {                        
            await TryUpdateModelAsync<UserTable>(
                             updateTable,
                             "updateTable",
                             s => s.U_ID, s => s.f_name, s => s.l_name, s => s.Telephone_1, s => s.Telephone_2, s => s.Adress, s => s.Zipcode, s => s.City, s => s.County, s => s.Nationality);

            _context.UserTable.Attach(updateTable);

                await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
