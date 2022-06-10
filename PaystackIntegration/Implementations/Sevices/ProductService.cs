using Microsoft.AspNetCore.Hosting;
using PaystackIntegration.Dtos;
using PaystackIntegration.Interfaces.IRepositories;
using PaystackIntegration.Interfaces.IServices;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Implementations.Sevices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public ProductDto CreateNew(CreateProductRequestModel model)
        {
            var product = new Product
            {
                Name = model.Name,

                Price = model.Price,

                Description = model.Description
            };

            if (model.file != null)
            {
                string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "productImages");
                Directory.CreateDirectory(imageDirectory);
                string contentType = model.file.ContentType.Split('/')[1];
                string imageUrl = $"{Guid.NewGuid()}.{contentType}";
                string fullPath = Path.Combine(imageDirectory, imageUrl);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.file.CopyTo(fileStream);
                }
                product.ImageUrl = imageUrl;
            }

            return _productRepository.Create(product);
        }

        public List<ProductDto> GetAll()
        {
            return _productRepository.GetAll();
        }

        public async Task<IList<Product>> GetAllSelectedProducts(IEnumerable<int> ids)
        {
            return await _productRepository.GetOrderProducts(ids);
        }

        public ProductDto GetById(int id)
        {
            return _productRepository.GetById(id);
        }
    }
}
