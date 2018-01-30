using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace wedding_planner.Models
{
    public class RegisterViewModels
    {
        [Required]
        [Display(Name = "First Name: ")]
        [MinLength(2)]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last Name: ")]
        [MinLength(2)]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Email: ")]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        [MinLength(2)]
        public string password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string conPassword { get; set; }
    }
}