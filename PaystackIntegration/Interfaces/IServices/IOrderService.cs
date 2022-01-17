using PaystackIntegration.Dtos;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Interfaces.IServices
{
    public interface IOrderService
    {
        public OrderDto GetById(int id);

        public List<OrderDto> GetAll();

        public Order Find(int id);

        public List<OrderDto> GetAllCustomerOrder(int id);

        public Task<OrderDto> GetByReference(string reference);

        public Task<Order> GetOrderByRef(string reference);

        public Task<OrderDto> CreateOrder(Order order);

        public void UpdateOrder(Order order);
    }
}
