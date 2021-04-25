using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoboVet6.Data.DbContext;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;

namespace RoboVet6.DataAccess.Repositories
{
    public class ProductRepository : IProductIRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<ProductModel>> GetAllProducts(string name)
        {
            var collection = _context.Products as IQueryable<ProductModel>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                collection = collection.Where(x => x.Name.Contains(name));
            }

            return await collection.ToListAsync();
        }

        public async Task<ProductModel> GetProductByProductId(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task InsertProduct(ProductModel product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(ProductModel product)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ProductExists(int productId)
        {
            var existingProduct = await _context.Products.FindAsync(productId);

            return existingProduct != null;
        }
    }
}
