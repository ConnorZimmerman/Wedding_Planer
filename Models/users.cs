using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding_planner.Models
{
    public class users
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        [InverseProperty("user")]
        public List<weddings_has_users> weddings_has_users { get; set; }
        public users()
        {
            weddings_has_users = new List<weddings_has_users>();
        }
    }
}
