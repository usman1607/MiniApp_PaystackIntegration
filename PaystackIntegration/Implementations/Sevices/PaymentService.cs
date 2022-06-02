using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaystackIntegration.Dtos;
using PaystackIntegration.Enums;
using PaystackIntegration.Extensions;
using PaystackIntegration.Interfaces.IRepositories;
using PaystackIntegration.Interfaces.IServices;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PaystackIntegration.Implementations.Sevices
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public PaymentService(IConfiguration config, IProductService productService, IOrderService orderService, ICustomerService customerService)
        {
            _config = config;
            _client = new HttpClient();
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }

        public async Task<OrderDto> CheckOut(CreateOrderRequestModel model)
        {
            var productDictionary = model.orderProducts.ToDictionary(o => o.ProductId);
            var products = await _productService.GetAllSelectedProducts(productDictionary.Keys);

            var customer = _customerService.Find(model.CustomerId);
            var order = new Order
            {
                Customer = customer,
                CustomerId = model.CustomerId,
                Date = DateTime.UtcNow,
                DeliveryAddress = model.DeliveryAddress,
                Reference = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "").ToUpper(),
                Status = OrderStatus.Default
            };
            foreach (var product in products)
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

            var orderDto = await _orderService.CreateOrder(order);

            var response = await InitializePaystackPayment(orderDto);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                //return Payment not completed error...
            }

            return orderDto;
        }

        public async Task<HttpResponseMessage> InitializePaystackPayment(OrderDto orderDto)
        {
            var curl_url = $"{_config["Api:Url"]}api/v1/payment/{orderDto.Reference}";
            var model = new PaystackPayRequestModel
            {
                email = orderDto.CustomerEmail,
                amount = orderDto.TotalPrice * 100,
                reference = orderDto.Reference,
                //metadata = orderDto.OrderProducts,
                phoneNumber = orderDto.CustomerPhone,
                fullName = orderDto.CustomerName,
                label = orderDto.CustomerName,
                callback = curl_url
            };

            var url = $"https://api.paystack.co/transaction/initialize";
            var key = _config["Api:MyKey"];

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", key);

                //var payload = JsonConvert.SerializeObject(model);
                var payload = System.Text.Json.JsonSerializer.Serialize(model);

                requestMessage.Content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(requestMessage);

                return response;
            }
        }

        public async Task<PaymentDto> VerifyPayment(string reference)
        {
            PaymentDto payment = null;
            var url = $"https://api.paystack.co/transaction/verify/{reference}";
            var key = _config["Api:MyKey"];

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", key);
                var response = await _client.SendAsync(request);
                var responseModel = await response.ReadContentAs<PaystackResponse>();

                if (responseModel.Status)
                {
                    var order = await _orderService.GetOrderByRef(reference);

                    order.Status = OrderStatus.InProgress;
                    order.Paid = true;
                    order.PaidAt = DateTime.Parse(responseModel.Paid_At);

                    _orderService.UpdateOrder(order);

                    payment = new PaymentDto
                    {
                        CustomerId = order.CustomerId,
                        CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}",
                        Reference = order.Reference,
                        Amount = order.TotalPrice,
                        Date = order.PaidAt,
                        OrderId = order.Id
                    };
                }
            }

            return payment;
        }
    }
}
