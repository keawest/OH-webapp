using System;
using System.ComponentModel.DataAnnotations;

namespace OptimalHealthApp.Models
{
    public class Appointment
    {
    [Key]
    public int D_ID { get; set; }
    public int U_ID { get; set; }
    public DateTime Date_of_appointment { get; set; }
    public String Time_of_appintment { get; set; }
    public String Doctor_notes { get; set; }  
    public String Patient_notes { get; set; } 
    }

}
