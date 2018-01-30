using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding_planner.Models
{
    public class weddings
    {
        public int id {get;set;}
        public string wedders { get; set; }
        public DateTime date { get; set; }
        public string address { get; set; }
        public int guests { get; set; }
        public int created_by { get; set; }

        [InverseProperty("wedding")]
        public List<weddings_has_users> weddings_has_users { get; set; }
        public weddings()
        {
            weddings_has_users = new List<weddings_has_users>();
        }
    }
}
