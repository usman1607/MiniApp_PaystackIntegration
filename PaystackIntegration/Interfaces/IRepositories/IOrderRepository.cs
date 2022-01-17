using PaystackIntegration.Dtos;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Interfaces.IRepositories
{
    public interface IOrderRepository
    {
        public OrderDto GetById(int id);

        public Task<OrderDto> Create(Order product);

        public List<OrderDto> GetAll();

        public Order Find(int id);

        public void Update(Order order);

        public Task<OrderDto> GetByReference(string reference);

        public Task<Order> FindByReference(string reference);

        public List<OrderDto> GetAllCustomerOrder(int customerId);

        //Payments...
        public Task<List<PaymentDto>> GetAllPayments();

        public Task<PaymentDto> GetPaymentByReference(string reference);

        public Task<List<PaymentDto>> GetAllCustomerPayment(int id);
    }
}
