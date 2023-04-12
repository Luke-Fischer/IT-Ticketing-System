using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IT_Ticketing_System.Models
{
    public class AdminRegister
    {
        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [DisplayName("Company Password")]
        public string CompanyPassword { get; set; }
        [Required]
        [DisplayName("Retype Company Password")]
        public string RetypeCompanyPassword { get; set; }
    }
}
