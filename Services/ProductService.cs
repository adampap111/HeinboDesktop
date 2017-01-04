using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Zza.Data;

namespace HeinboDesktop.Services
{
    public class ProductService : IProductService
    {
        HeinboDataContext _context = new HeinboDataContext();

        public Task<List<Product>> GetProducts()
        {
            return _context.Products.ToListAsync();
        }

        public Task<Product> GetProductById(int id)
        {
            return _context.Products.FirstOrDefaultAsync(c => c.ProductId == id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            if (!_context.Products.Local.Any(c => c.ProductId == product.ProductId))
            {
                _context.Products.Attach(product);
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(c => c.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            await _context.SaveChangesAsync();
        }

    

      
    }
}
