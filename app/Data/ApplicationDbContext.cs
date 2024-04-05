using Microsoft.EntityFrameworkCore;

using sample_api_csharp.Models;

namespace sample_api_csharp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        public DbSet<Customer> Customers {  get; set; }
     
    }
}