using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PortFolio_A1_Version2._0.Models
{
    public class SendEmail
    {

        [Required(ErrorMessage = "To Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "To Email")]
        public string ToEmail { get; set; }


        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(100, ErrorMessage = "Subject cannot be longer than 100 characters.")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "Content is required.")]
        [StringLength(5000, ErrorMessage = "Content cannot be longer than 5000 characters.")]
        public string Content { get; set; }
    }
}