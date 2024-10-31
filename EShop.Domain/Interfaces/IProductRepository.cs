using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public  interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetProductByIdAsync(int id);
        List<ProductImage> GetProductImagesByIdAsync(int productId);
        Task InsertAsync(Product product);
        Task  InsertProductImagesAsync(ProductImage image);
        Task SaveAsync();
        void Update(Product product);
        void DeleteProductImages(ProductImage image);
        Task Delete(int id);
        //Task DeleteUser(int id);
        //Task<bool> IsUserExistsByEmail(string email);
        //Task<User> GetUserByEmail(string email);
        //Task<User> GetUserByConfirmCode(string ConfirmCode);
    }
}
