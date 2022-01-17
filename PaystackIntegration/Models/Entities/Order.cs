using PaystackIntegration.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string Reference { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Delivery address is required")]
        [StringLength(300)]
        public string DeliveryAddress { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public bool Paid { get; set; }

        public DateTime PaidAt { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
