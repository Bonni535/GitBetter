using GitBetter.Models;

namespace GitBetter.Data
{
    public class AppointmentData
    {
        public static List<Appointment> Appointments = new List<Appointment>
        {
            new Appointment { Id = 1, PatientId = 1, ProviderId = 2, FacilityId = 1, Date = new DateTime(2024, 08, 01), IsScheduled = true, Notes = ""},
            new Appointment { Id = 2, PatientId = 2, ProviderId = 1, FacilityId = 2, Date = new DateTime(2024, 07, 03), IsScheduled = true, Notes = ""},
        };
    }
}