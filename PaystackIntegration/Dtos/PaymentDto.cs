using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Dtos
{
    public class PaymentDto
    {
        public string Reference { get; set; }

        public string CustomerName { get; set; }

        public int CustomerId { get; set; }

        public string OrderReference { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }

    public class PaystackPayRequestModel
    {
        public string email { get; set; }
        public decimal amount { get; set; }
        public string reference { get; set; }
        public List<OrderProductDto> metadata { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string label { get; set; }
        public string callback { get; set; }
    }

    public class CheckoutResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string PaymentUrl { get; set; }
    }

    public class PaystackResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public UrlInfo Data { get; set; }

    }

    public class UrlInfo
    {
        public string Authorization_url { get; set; }
        public string Access_code { get; set; }
        public string Reference { get; set; }
    }

    public class PaystackVerificationResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public long Id { get; set; }
        public string Domain { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
        public string Gateway_Response { get; set; }
        public string Paid_At { get; set; }
        public string Created_At { get; set; }
        public string Channel { get; set; }
        public string Currency { get; set; }
        public string Ip_Address { get; set; }
    }
}
