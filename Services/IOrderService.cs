using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeinboDesktop.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();
    }
}
