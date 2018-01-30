using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace wedding_planner.Models
{
    //AIzaSyBzR0QdrBrfOJgJdHI5SlC5Bp2ekXVjCuw
    public class WeddingViewModels
    {
        [Required]
        [Display(Name = "Wedder One ")]
        public string wedder_one { get; set; }

        [Required]
        [Display(Name = "Wedder Two ")]
        public string wedder_two { get; set; }

        [Required]
        [Display(Name = "Date ")]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Date cannot be a future time!")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Weddings Address")]
        public string wedding_address { get; set; }

    }
}