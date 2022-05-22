using System.ComponentModel.DataAnnotations;

namespace TicketSystemApi.Persistance.Data
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "This Filed Is Required")]
        [StringLength(150, ErrorMessage = "Invalid Maximum Length is 150")]
        [MaxLength(150, ErrorMessage = "Invalid Input Maximum length 150")]
        [MinLength(4, ErrorMessage = "Invalid Input Minimum length 4")]
        public string Username { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Invalid Maximum Length is 150")]
        [MaxLength(150, ErrorMessage = "Invalid Input Maximum length 150")]
        [MinLength(4, ErrorMessage = "Invalid Input Minimum length 4")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [RegularExpression("^01[0-2,5]{1}[0-9]{8}$")]
        public string PhoneNumber { get; set; }
    }
}
