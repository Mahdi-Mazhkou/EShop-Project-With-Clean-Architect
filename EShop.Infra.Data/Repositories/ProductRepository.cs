using EShop.Domain.Interfaces;
using EShop.Domain.Models;
using EShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyEShopContext _context;

        public ProductRepository(MyEShopContext context)
        {
            _context = context;
        }

       
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.ProductGroup).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductGroup)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public List<ProductImage> GetProductImagesByIdAsync(int productId)
        {

            return _context.ProductImages.Where(x => x.ProductId == productId).ToList();

        }

        public async Task InsertAsync(Product product)
        {

            await _context.Products.AddAsync(product);
        }

        public async Task InsertProductImagesAsync(ProductImage image)
        {
            await _context.ProductImages.AddAsync(image);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
        public async Task Delete(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                product.IsDelete = true;
            
            }
        }

        public void DeleteProductImages(ProductImage image)
        {
            _context.ProductImages.Remove(image);
        }
    }
}
