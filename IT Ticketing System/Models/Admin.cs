﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace IT_Ticketing_System.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [DisplayName("Company Password")]
        public string CompanyPassword { get; set; }
        [AllowNull]
        public string CompanyUniqueIdentifer { get; set; }
    }
}
