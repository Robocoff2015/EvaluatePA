using Microsoft.EntityFrameworkCore;

namespace EvaluatePa.Models

{
    public class ConnectStringClass : DbContext
    {
        public ConnectStringClass(DbContextOptions<ConnectStringClass> options) :base(options)
        {
        }

        public DbSet<DevelopPA> DevelopPA { get; set; }
    }
}
