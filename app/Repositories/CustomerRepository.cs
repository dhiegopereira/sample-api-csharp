using Microsoft.EntityFrameworkCore;
using sample_api_csharp.Abstracts;
using sample_api_csharp.Data;
using sample_api_csharp.Models;

namespace sample_api_csharp.Repositories
{
    public class CustomerRepository : AbstractRepository<Customer>
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Customer> FilterByName(string name)
        {
            return _context.Customers.Where(c => EF.Functions.Like(c.Name, $"%{name}%")).ToList();
        }
    }
}
