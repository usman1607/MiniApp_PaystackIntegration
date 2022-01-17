using PaystackIntegration.Dtos;
using PaystackIntegration.Interfaces.IRepositories;
using PaystackIntegration.Interfaces.IServices;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Implementations.Sevices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer Find(int id)
        {
            return _customerRepository.Find(id);
        }

        public List<CustomerDto> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public CustomerDto GetByEmail(string email)
        {
            return _customerRepository.GetByEmail(email);
        }

        public CustomerDto GetById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public CustomerDto Register(CreateCustomerRequestModel model)
        {
            if (_customerRepository.Exists(model.Email))
            {
                //Throw custom already exit exception...
                return null;
            }
            else
            {
                var customer = new Customer
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };

                return _customerRepository.Create(customer);
            }
        }
    }
}
