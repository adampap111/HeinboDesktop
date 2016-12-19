using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Zza.Data;

namespace HeinboDesktop.Services
{
    public class CustomersRepository : ICustomersRepository
    {
        ZzaDbContext _context = new ZzaDbContext();

        public Task<List<Product>> GetCustomersAsync()
        {
            return _context.Products.ToListAsync();
        }

        public Task<Product> GetCustomerAsync(int id)
        {
            return _context.Products.FirstOrDefaultAsync(c => c.ProductId == id);
        }

        public async Task<Product> AddCustomerAsync(Product customer)
        {
            _context.Products.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Product> UpdateCustomerAsync(Product customer)
        {
            if (!_context.Products.Local.Any(c => c.ProductId == customer.ProductId))
            {
                _context.Products.Attach(customer);
            }
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;

        }

        public async Task DeleteCustomerAsync(int productId)
        {
            var product = _context.Products.FirstOrDefault(c => c.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            await _context.SaveChangesAsync();
        }

        public Task<List<Order>> GetOrdersAsync()
        {
            return _context.Order.ToListAsync();
        }

        public Task<List<AspNetUsers>> GetCustomerssAsync()
        {
            return _context.User.ToListAsync();
        }
    }
}
