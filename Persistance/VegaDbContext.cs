using AspNetBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetBasics.Persistance
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features{get;set;}

        public VegaDbContext(DbContextOptions<VegaDbContext> options):base(options){
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => new {vf.VehicleId, vf.FeatureId});
        }
    }
}