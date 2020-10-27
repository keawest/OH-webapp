using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OptimalHealthApp.Models;

namespace OptimalHealthApp
{
    [Authorize(Roles = "Doctor")]
    public class doctorProfilePageModel : PageModel
    {
        private readonly OptimalHealthApp.Data.OptimalHealthAppContext _context;

        public doctorProfilePageModel(OptimalHealthApp.Data.OptimalHealthAppContext context)
        {
            _context = context;
        }

        public Doctor Doctor { get; set; }
        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Doctor = await _context.Doctor.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);

            var loggedInDoctorEmail = User.Identity.Name;
            var doctorID = (from Doctor in _context.Doctor
                        .Where(u => u.Email == loggedInDoctorEmail) //Finds logged in Doctors D_ID
                          select Doctor.D_ID).FirstOrDefault();

            Appointment = await _context.Appointment.FirstOrDefaultAsync(m => m.D_ID == doctorID);//shows logged in users last appointment

            if (Doctor == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
