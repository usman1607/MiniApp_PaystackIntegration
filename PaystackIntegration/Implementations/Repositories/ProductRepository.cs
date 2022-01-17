using Microsoft.EntityFrameworkCore;
using PaystackIntegration.Context;
using PaystackIntegration.Dtos;
using PaystackIntegration.Interfaces.IRepositories;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Implementations.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductDto Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return new ProductDto
            {
                Id = product.Id,
       
                Name = product.Name,

                Price = product.Price,

                ImageUrl = product.ImageUrl,

                Description = product.Description
            };
        }

        public bool Exists(string name)
        {
            return _context.Products.Any(p => p.Name == name);
        }

        public List<ProductDto> GetAll()
        {
            return _context.Products.Select(product => new ProductDto
            {
                Id = product.Id,

                Name = product.Name,

                Price = product.Price,

                ImageUrl = product.ImageUrl,

                Description = product.Description
            }).ToList();
        }

        public ProductDto GetById(int id)
        {
            return _context.Products.Where(p => p.Id == id).Select(product => new ProductDto 
            {
                Id = product.Id,

                Name = product.Name,

                Price = product.Price,

                ImageUrl = product.ImageUrl,

                Description = product.Description
            }).SingleOrDefault();
        }

        public async Task<IList<Product>> GetOrderProducts(IEnumerable<int> ids)
        {
            return await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
    }
}
