using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zza.Data;

namespace HeinboDesktop.Services
{
    public interface ICustomersRepository
    {
        Task<List<Product>> GetCustomersAsync();
        Task<List<Order>> GetOrdersAsync();
        Task<Product> GetCustomerAsync(int id);
        Task<Product> AddCustomerAsync(Product customer);
        Task<Product> UpdateCustomerAsync(Product customer);
        Task DeleteCustomerAsync(int customerId);
        Task<List<AspNetUsers>> GetCustomerssAsync();
    }
}
