using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OptimalHealthApp.Models;

namespace OptimalHealthApp
{
    [Authorize(Roles = "Doctor")]
    public class Patient_RecordModel : PageModel
    {
        private readonly OptimalHealthApp.Data.OptimalHealthAppContext _context;

        public Patient_RecordModel(OptimalHealthApp.Data.OptimalHealthAppContext context)
        {
            _context = context;
        }
        public string CurrentFilter { get; set; }

          public IList<Appointment> Appointment { get;set; } 

          public async Task OnGetAsync(string searchString)
          {
              CurrentFilter = searchString;
              IQueryable<Appointment> Doctor_notes = from Appointment in _context.Appointment select Appointment;
              Appointment = await _context.Appointment.ToListAsync();

              if (!String.IsNullOrEmpty(searchString))
              {
                  Doctor_notes = Doctor_notes.Where(Appointment => Appointment.Doctor_notes.Contains(searchString));
              }
              Appointment = await Doctor_notes.AsNoTracking().ToListAsync();
          }
    }
}
