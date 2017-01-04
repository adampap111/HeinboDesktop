using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeinboDesktop.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product customer);
        Task<Product> UpdateProduct(Product customer);
        Task DeleteProduct(int customerId);

    }
}
