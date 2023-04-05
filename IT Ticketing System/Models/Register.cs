using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IT_Ticketing_System.Models
{
    public class Register
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must contain between 8 and 30 ")]
        public string Password { get; set; }
        
        [Required]
        [DisplayName("Retype Password")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must contain between 8 and 30 ")]
        public string retypePassword { get; set; }
    }
}
