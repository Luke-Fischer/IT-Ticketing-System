using System.ComponentModel.DataAnnotations;

namespace IT_Ticketing_System.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must contain between 8 and 30 characters.")]
        public string Password { get; set; }
    }
}
