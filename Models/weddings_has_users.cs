using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding_planner.Models
{
    public class weddings_has_users
    {
        public int id { get; set; }

        [ForeignKey("user")]
        public int users_id { get; set; }
        public users user { get; set; }
        
        [ForeignKey("wedding")]
        public int weddings_id { get; set; }
        public weddings wedding { get; set; }
    }
}