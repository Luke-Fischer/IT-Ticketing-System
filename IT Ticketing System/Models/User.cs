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

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
