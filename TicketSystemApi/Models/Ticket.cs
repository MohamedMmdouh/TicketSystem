using System;
using System.ComponentModel.DataAnnotations;

namespace TicketSystemApi.Models
{
    public class Ticket
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Location { get; set; }
        public string imagepath { get; set; }
        [Required]
        public string employeename { get; set; }
        public int TIcketNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0: HH:mm:ss}")]
        public TimeSpan StartTime { get; set; }
        [DisplayFormat(DataFormatString = "{0: HH:mm:ss}")]
        public TimeSpan EndTime { get; set; }
        public string  userid { get; set;}
        public User user { get; set; }
    }
}
