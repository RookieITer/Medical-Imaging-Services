using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortFolio_A1_Version2._0.Models
{
    public class DoctorDetails
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string DoctorName { get; set; }

        [Required]
        [RegularExpression(@"^04\d{8}$", ErrorMessage = "Phone number must be an Australian mobile number starting with '04' and have 10 digits in total.")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0, 8, ErrorMessage = "Working hours must be between 0 and 8.")]
        public string WorkingHours { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

    }
}