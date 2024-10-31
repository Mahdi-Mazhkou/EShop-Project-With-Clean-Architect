using EShop.Domain.Models;
using EShop.Domain.ViewModels.User;
using EShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.ViewModels.Product;
using Microsoft.AspNetCore.Http;


namespace EShop.Application.Services.Interfaces
{
    public  interface IProductService
    {
        Task<List<ListOfProductViewModel>> GetListOfProducts();
        Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int id);
        Task<ResultCreateProduct> CreateProductAsync(CreateProductViewModel model, IFormFile[] imgUp);
        Task<EditProductViewModel> GetProductByIdForEditAsync(int id);
        Task<DeleteProductViewModel> GetProductByIdForDeleteAsync(int id);
        List<ProductImage> GetProductImagesAsync(int productId);
        Task<ResultEditProduct> UpdateProductAsync(int id,EditProductViewModel model, IFormFile[] imgUp);
        Task<ResultDeleteProduct> DeleteProductAsync(int id);
    }
}
