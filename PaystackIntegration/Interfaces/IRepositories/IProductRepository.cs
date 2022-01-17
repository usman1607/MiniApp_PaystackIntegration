using PaystackIntegration.Dtos;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Interfaces.IRepositories
{
    public interface IProductRepository
    {
        public ProductDto GetById(int id);

        public ProductDto Create(Product product);

        public List<ProductDto> GetAll();

        public bool Exists(string name);

        public Task<IList<Product>> GetOrderProducts(IEnumerable<int> ids);
    }
}
