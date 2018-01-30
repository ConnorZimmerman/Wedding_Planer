using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace wedding_planner.Models
{
    public class LoginViewModels
    {
        [Required]
        [Display(Name = "Email: ")]
        [EmailAddress]
        [MinLength(2)]
        public string lEmail { get; set; }

        [Required]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        [MinLength(2)]
        public string lPassword { get; set; }
    }
}