using AboHalalBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AboHalalBackend.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        
 
       public DbSet<User> Users => Set<User>();
      public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        
    }
}
