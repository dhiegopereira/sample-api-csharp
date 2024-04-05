using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sample_api_csharp.Abstracts;
using sample_api_csharp.Data;
using sample_api_csharp.DTOs;
using sample_api_csharp.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace sample_api_csharp.Repositories
{
    public class LoginRepository : AbstractRepository<Customer>
    {
        private readonly ApplicationDbContext _context;
        public LoginRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Customer Auth(LoginDTO login)
        {
            Customer customer = _context.Customers.SingleOrDefault(u => u.Email == login.Email!);

            if (customer == null || !BCrypt.Net.BCrypt.Verify(login.Password, customer.Password))
                return null;


            return customer;
        }
    }
}
