using Microsoft.EntityFrameworkCore;

namespace AspNetBasics.Persistance
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options):base(options){
            
        }
    }
}