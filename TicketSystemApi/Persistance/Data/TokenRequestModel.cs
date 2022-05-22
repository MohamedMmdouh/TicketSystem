using System.ComponentModel.DataAnnotations;

namespace TicketSystemApi.Persistance.Data
{
    public class TokenRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
