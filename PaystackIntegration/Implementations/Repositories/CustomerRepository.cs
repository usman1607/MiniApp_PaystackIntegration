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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public CustomerDto Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return new CustomerDto
            {
                Id = customer.Id,

                FirstName = customer.FirstName,

                LastName = customer.LastName,

                PhoneNumber = customer.PhoneNumber,

                Address = customer.Address,

                Email = customer.Email
            };
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public bool Exists(string email)
        {
            return _context.Customers.Any(c => c.Email == email);
        }

        public Customer Find(int id)
        {
            return _context.Customers.Find(id);
        }

        public List<CustomerDto> GetAll()
        {
            return _context.Customers.Include(c => c.Orders).ThenInclude(o => o.OrderProducts)
                .Select(customer => new CustomerDto
                {
                    Id = customer.Id,

                    FirstName = customer.FirstName,

                    LastName = customer.LastName,

                    PhoneNumber = customer.PhoneNumber,

                    Address = customer.Address,

                    Email = customer.Email,

                    Orders = customer.Orders
                }).ToList();
        }

        public CustomerDto GetByEmail(string email)
        {
            return _context.Customers.Where(c => c.Email == email).Include(c => c.Orders)
                .Select(customer => new CustomerDto
                {
                    Id = customer.Id,

                    FirstName = customer.FirstName,

                    LastName = customer.LastName,

                    PhoneNumber = customer.PhoneNumber,

                    Address = customer.Address,

                    Email = customer.Email,

                    Orders = customer.Orders
                }).SingleOrDefault();
        }

        public CustomerDto GetById(int id)
        {
            return _context.Customers.Where(c => c.Id == id).Include(c => c.Orders)
                .Select(customer => new CustomerDto
                {
                    Id = customer.Id,

                    FirstName = customer.FirstName,

                    LastName = customer.LastName,

                    PhoneNumber = customer.PhoneNumber,

                    Address = customer.Address,

                    Email = customer.Email,

                    Orders = customer.Orders
                }).SingleOrDefault();
        }

        public CustomerDto Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return new CustomerDto
            {
                Id = customer.Id,

                FirstName = customer.FirstName,

                LastName = customer.LastName,

                PhoneNumber = customer.PhoneNumber,

                Address = customer.Address,

                Email = customer.Email,

                Orders = customer.Orders
            };

        }
    }
}
