using System.ComponentModel.DataAnnotations;

namespace TicketSystemApi.Persistance.Data
{
    public class TicketViewModel
    {
        [Required]
        public string Location { get; set; }
        public string imagepath { get; set; }

        [Required]
        public string employeename { get; set; }

        [Required(ErrorMessage = "Time is required")]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]?$", ErrorMessage = "Please enter a valid time")]
        public string StartTime { get; set; }  
        
        [Required(ErrorMessage = "Time is required")]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]?$", ErrorMessage = "Please enter a valid time")]
        public string EndTime { get; set; }
        [Required(ErrorMessage = "User is required")]
        public string userid { get; set; }
    }
}
