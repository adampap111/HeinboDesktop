using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heinbo.Data;
using System.Data.Entity;

namespace HeinboDesktop.Services
{
    public class CustomerService : ICustomerService
    {

        HeinboDataContext _context;
        public CustomerService(HeinboDataContext context)
        {
            _context = context;
        }

        public Task<List<AspNetUsers>> GetCustomerList()
        {
            return _context.User.ToListAsync();
        }
      
    }
}
