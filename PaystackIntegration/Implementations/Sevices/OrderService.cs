using PaystackIntegration.Dtos;
using PaystackIntegration.Enums;
using PaystackIntegration.Interfaces.IRepositories;
using PaystackIntegration.Interfaces.IServices;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaystackIntegration.Implementations.Sevices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> CreateOrder(Order order)
        {
            return await _orderRepository.Create(order);
        }

        public Order Find(int id)
        {
            return _orderRepository.Find(id);
        }

        public List<OrderDto> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public List<OrderDto> GetAllCustomerOrder(int id)
        {
            return _orderRepository.GetAllCustomerOrder(id);
        }

        public OrderDto GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public async Task<OrderDto> GetByReference(string reference)
        {
            return await _orderRepository.GetByReference(reference);
        }

        public Task<Order> GetOrderByRef(string reference)
        {
            return _orderRepository.FindByReference(reference);
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
        }
    }
}
