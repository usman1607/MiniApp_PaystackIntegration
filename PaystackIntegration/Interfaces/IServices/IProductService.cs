using PaystackIntegration.Dtos;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Interfaces.IServices
{
    public interface IProductService
    {
        public ProductDto GetById(int id);

        public ProductDto CreateNew(CreateProductRequestModel model);

        public List<ProductDto> GetAll();

        public Task<IList<Product>> GetAllSelectedProducts(IEnumerable<int> ids);
    }
}
