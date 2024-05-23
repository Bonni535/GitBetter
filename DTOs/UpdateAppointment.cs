using GitBetter.Models;

namespace GitBetter.Dtos
{
    public class UpdateAppointmentDTO
    {
        public DateTime Date { get; set; }
        public Boolean IsScheduled { get; set; }
        public string Notes { get; set; }


    }
}