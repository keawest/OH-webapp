using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OptimalHealthApp.Models;

namespace OptimalHealthApp
{
    [Authorize(Roles = "Patient")]
    public class PatientProfilePageModel : PageModel
    {
        private readonly OptimalHealthApp.Data.OptimalHealthAppContext _context;

        public PatientProfilePageModel(OptimalHealthApp.Data.OptimalHealthAppContext context)
        {
            _context = context;
        }

        public UserTable UserTable { get; set; }
        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UserTable = await _context.UserTable.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);//shows user information

            var loggedInUserEmail = User.Identity.Name;
             var userID = (from UserTable in _context.UserTable
                         .Where(u => u.Email == loggedInUserEmail) //Finds logged in users U_ID
                           select UserTable.U_ID).FirstOrDefault();

            Appointment = await _context.Appointment.FirstOrDefaultAsync(m => m.U_ID == userID);//shows logged in users last appointment
             

            if (UserTable == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
