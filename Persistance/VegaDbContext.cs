using AspNetBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetBasics.Persistance
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options):base(options){
            
        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features{get;set;}
    }
}