using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IT_Ticketing_System.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string CompanyPassword { get; set; } 
        public string CompanyUniqueIdentifer { get; set; }
    }
}
