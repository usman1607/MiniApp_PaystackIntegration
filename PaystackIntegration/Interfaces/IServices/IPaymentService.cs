using PaystackIntegration.Dtos;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaystackIntegration.Interfaces.IServices
{
    public interface IPaymentService
    {
        public Task<CheckoutResponse> CheckOut(CreateOrderRequestModel model);

        public Task<HttpResponseMessage> InitializePaystackPayment(OrderDto orderDto);

        public Task<PaymentDto> VerifyPayment(string reference);
    }
}
