using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OptimalHealthApp.Models;

namespace OptimalHealthApp
{
    [Authorize(Roles = "Patient")]
    public class BooookModel : PageModel
    {
        private readonly OptimalHealthApp.Data.OptimalHealthAppContext _context;


        public BooookModel(OptimalHealthApp.Data.OptimalHealthAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public IActionResult OnGet()
        {
            var userID = User.Identity.Name;//gets the logged in users email
            var user = (from UserTable in _context.UserTable
                        .Where(u => u.Email == userID)select UserTable.U_ID)
                        .FirstOrDefault();//gets the U_ID number that correspondes with logged in users email from DB
            
            ViewData["id"] = user;//displays U_ID in the view
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
