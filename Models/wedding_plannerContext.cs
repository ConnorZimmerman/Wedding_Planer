using Microsoft.EntityFrameworkCore;

namespace wedding_planner.Models
{
    public class wedding_plannerContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public wedding_plannerContext(DbContextOptions<wedding_plannerContext> options) : base(options) { }

        // public DbSet<ModelName> TableName {get;set;}
        public DbSet<users> users {get; set;}
        public DbSet<weddings> weddings {get; set;}
        public DbSet<weddings_has_users> weddings_has_users { get; set; }
    }
}