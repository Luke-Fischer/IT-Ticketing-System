﻿using System.ComponentModel.DataAnnotations;
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
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must contain between 8 and 30 characters.")]
        [DisplayName("Company Password")]
        
        public string CompanyPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must contain between 8 and 30 characters.")]
        [DisplayName("Retype Company Password")]
        public string RetypeCompanyPassword { get; set; }
    }
}
