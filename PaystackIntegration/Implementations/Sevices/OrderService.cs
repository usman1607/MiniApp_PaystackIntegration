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
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository /*, IProductRepository productRepository, ICustomerRepository customerRepository, IPaymentService paymentService*/)
        {
            //_paymentService = paymentService;
            _orderRepository = orderRepository;
            //_productRepository = productRepository;
            //_customerRepository = customerRepository;
        }

        public async Task<OrderDto> CreateOrder(Order order)
        {
            return await _orderRepository.Create(order);
        }

        /* public async Task<OrderDto> CheckOut(CreateOrderRequestModel model)
         {
             var productDictionary = model.orderProducts.ToDictionary(o => o.ProductId);
             var products = _productRepository.GetOrderProducts(productDictionary.Keys);

             var customer = _customerRepository.Find(model.CustomerId);
             var order = new Order
             {
                 Customer = customer,
                 CustomerId = model.CustomerId,
                 Date = DateTime.UtcNow,
                 DeliveryAddress = model.DeliveryAddress,
                 Reference = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "").ToUpper(),
                 Status = OrderStatus.Default
             };
             foreach(var product in products)
             {
                 var quantity = productDictionary[product.Id].Quantity;
                 var orderProduct = new OrderProduct
                 {
                     ProductId = product.Id,
                     Product = product,
                     Order = order,
                     OrderId = order.Id,
                     Quantity = quantity,
                     UnitPrice = product.Price
                 };
                 order.TotalPrice += product.Price * quantity;
                 order.OrderProducts.Add(orderProduct);
             }

             var orderDto = _orderRepository.Create(order);
             var response = await _paymentService.InitializePaystackPayment(orderDto);

             if (response.StatusCode == HttpStatusCode.OK)
             {
                 order.Status = OrderStatus.InProgress;
                 _orderRepository.Update(order);
             }

             return orderDto;
         }*/

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
