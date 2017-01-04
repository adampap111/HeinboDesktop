using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeinboDesktop.Orders
{
    public class OrderProfile : BindableBase
    {
        public int OrderId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public virtual IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderStatus OrderStatus { get; set; }
        public int TotalPrice { get; set; }
        public string UserId { get; set; }
        public virtual string User { get; set; }
    }
}
