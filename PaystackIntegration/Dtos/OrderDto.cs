using PaystackIntegration.Enums;
using PaystackIntegration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string Reference { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }

        public string DeliveryAddress { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public bool Paid { get; set; }

        public DateTime PaidAt { get; set; }

        public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    }

    public class CreateOrderRequestModel
    {
        public int CustomerId { get; set; }

        public string DeliveryAddress { get; set; }

        public IEnumerable<CheckoutOrder> orderProducts { get; set; }
    }

    public class CheckoutOrder
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
