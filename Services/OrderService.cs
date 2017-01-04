using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heinbo.Data;
using System.Data.Entity;

namespace HeinboDesktop.Services
{
    public class OrderService : IOrderService
    {
        HeinboDataContext _context = new HeinboDataContext();

        public Task<List<Order>> GetOrdersAsync()
        {
            return _context.Order.ToListAsync();
        }

      
    }
}
