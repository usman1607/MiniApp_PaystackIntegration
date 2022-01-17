using PaystackIntegration.Dtos;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Interfaces.IServices
{
    public interface ICustomerService
    {
        public CustomerDto Register(CreateCustomerRequestModel model);
        public Customer Find(int id);
        public CustomerDto GetById(int id);
        public CustomerDto GetByEmail(string email);
        public List<CustomerDto> GetAll();
    }
}
