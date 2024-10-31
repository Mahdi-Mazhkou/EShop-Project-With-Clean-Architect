using EShop.Application.Services.Interfaces;
using EShop.Domain.Interfaces;
using EShop.Domain.Models;
using EShop.Domain.ViewModels.Product;
using EShop.Domain.ViewModels.User;
using EShop.Infra.Data.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<ListOfProductViewModel>> GetListOfProducts()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ListOfProductViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                CreateDate = p.CreateDate,
                Price = p.Price,
                IsDelete = p.IsDelete,
                GroupId = p.GroupId,
                ProductGroup = p.ProductGroup

            }).ToList();
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int id)
        {
            Product p = await _repository.GetProductByIdAsync(id);
            if (p == null)
                return null;

            return new ProductDetailsViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                CreateDate = p.CreateDate,
                Price = p.Price,
                IsDelete = p.IsDelete,
                GroupId = p.GroupId,
                ProductGroup = p.ProductGroup,
                Description = p.Description,
                ShortDescription = p.ShortDescription,
                Tags = p.Tags

            };
        }
        public async Task<ResultCreateProduct> CreateProductAsync(CreateProductViewModel model, IFormFile[] imgUp)
        {
            try
            {
                var product = new Product()
                {
                    CreateDate = DateTime.Now,
                    Description = model.Description,
                    Price = model.Price,
                    IsDelete = model.IsDelete,
                    GroupId = model.GroupId,
                    ProductGroup = model.ProductGroup,
                    ShortDescription = model.ShortDescription,
                    Tags = model.Tags,
                    Title = model.Title
                };
                await _repository.InsertAsync(product);
                await _repository.SaveAsync();

                if (imgUp != null && imgUp.Any())
                {
                    foreach (var img in imgUp)
                    {
                        string imageName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot/Images", imageName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            img.CopyTo(stream);
                        }

                        var images = new ProductImage()
                        {
                            CreateDate = DateTime.Now,
                            IsDelete = false,
                            ImageName = imageName,
                            ProductId = product.Id
                        };
                        await _repository.InsertProductImagesAsync(images);

                    }
                    await _repository.SaveAsync();
                }
                return ResultCreateProduct.Success;
            }
            catch (Exception)
            {

                return ResultCreateProduct.Failed;
            }
        }

        public async Task<EditProductViewModel> GetProductByIdForEditAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
                return null;

            return new EditProductViewModel()
            {
                Id = product.Id,
                Description = product.Description,
                GroupId = product.GroupId,
                IsDelete = product.IsDelete,
                Price = product.Price,
                ProductGroup = product.ProductGroup,
                ShortDescription = product.ShortDescription,
                Tags = product.Tags,
                Title = product.Title
            };
        }

        public List<ProductImage> GetProductImagesAsync(int productId)
        {
            return _repository.GetProductImagesByIdAsync(productId);
        }

        public async Task<ResultEditProduct> UpdateProductAsync(int id, EditProductViewModel newProduct, IFormFile[] imgUp)
        {
            try
            {
                var product = await _repository.GetProductByIdAsync(id);
                if (product != null)
                {
                    product.ProductGroup = newProduct.ProductGroup;
                    product.Price = newProduct.Price;
                    product.Description = newProduct.Description;
                    product.ShortDescription = newProduct.ShortDescription;
                    product.Title = newProduct.Title;
                    product.Tags = newProduct.Tags;
                    product.GroupId = newProduct.GroupId;

                    _repository.Update(product);
                    await _repository.SaveAsync();

                }

                if (imgUp != null && imgUp.Any())
                {
                    foreach (var img in imgUp)
                    {
                        string imageName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot/Images", imageName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            img.CopyTo(stream);
                        }

                        var images = new ProductImage()
                        {
                            CreateDate = DateTime.Now,
                            IsDelete = false,
                            ImageName = imageName,
                            ProductId = product.Id
                        };
                        await _repository.InsertProductImagesAsync(images);

                    }
                    await _repository.SaveAsync();
                }
                return ResultEditProduct.Success;


            }
            catch
            {

                return ResultEditProduct.Failed;
            }

        }

        public async Task<ResultDeleteProduct> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _repository.GetProductByIdAsync(id);
                if (product != null)
                {
                    await _repository.Delete(product.Id);
                    _repository.Update(product);

                }
                var images = _repository.GetProductImagesByIdAsync(product.Id);

                foreach (var image in images)
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(),
                                                  "wwwroot/Images", image.ImageName);
                    if (System.IO.File.Exists(deletePath))
                    {
                        System.IO.File.Delete(deletePath);
                    }
                    _repository.DeleteProductImages(image);
                }
                await _repository.SaveAsync();
                return ResultDeleteProduct.Success;
            }
            catch
            {

                return ResultDeleteProduct.Failed;
            }
        }

        public async Task<DeleteProductViewModel> GetProductByIdForDeleteAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
                return null;

            return new DeleteProductViewModel()
            {
                Id = product.Id,
                Description = product.Description,
                GroupId = product.GroupId,
                IsDelete = product.IsDelete,
                Price = product.Price,
                ProductGroup = product.ProductGroup,
                ShortDescription = product.ShortDescription,
                Tags = product.Tags,
                Title = product.Title,
                CreateDate = product.CreateDate
            };
        }
    }
}
