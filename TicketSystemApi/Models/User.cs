using Microsoft.AspNetCore.Identity;

namespace TicketSystemApi.Models
{
    public class User : IdentityUser
    {
        public Ticket ticket { get; set; }
    }
}
