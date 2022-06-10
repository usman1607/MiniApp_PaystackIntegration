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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> Create(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return new OrderDto
            {
                Id = order.Id,

                Reference = order.Reference,

                Date = order.Date,

                CustomerId = order.CustomerId,

                CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}",

                CustomerPhone = order.Customer.PhoneNumber,

                CustomerEmail = order.Customer.Email,

                DeliveryAddress = order.DeliveryAddress,

                TotalPrice = order.TotalPrice,

                Status = order.Status,

                Paid = order.Paid,

                PaidAt = order.PaidAt,

                OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                {
                    Id = op.Id,
                    OrderId = op.OrderId,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    UnitPrice = op.UnitPrice,
                    ProductName = op.Product.Name

                }).ToList()
            };
        }

        public Order Find(int id)
        {
            return _context.Orders.Find(id);
        }

        public List<OrderDto> GetAll()
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                .Select(order => new OrderDto
                {
                    Id = order.Id,

                    Reference = order.Reference,

                    Date = order.Date,

                    CustomerId = order.CustomerId,

                    CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}",

                    CustomerPhone = order.Customer.PhoneNumber,

                    CustomerEmail = order.Customer.Email,

                    DeliveryAddress = order.DeliveryAddress,

                    TotalPrice = order.TotalPrice,

                    Status = order.Status,

                    OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                    {
                        Id = op.Id,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        UnitPrice = op.UnitPrice,
                        ProductName = op.Product.Name

                    }).ToList()
                }).ToList();
        }

        public List<OrderDto> GetAllCustomerOrder(int custormerId)
        {
            return _context.Orders.Where(o => o.CustomerId == custormerId)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                .Select(order => new OrderDto
                {
                    Id = order.Id,

                    Reference = order.Reference,

                    Date = order.Date,

                    CustomerId = order.CustomerId,

                    CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}",

                    CustomerPhone = order.Customer.PhoneNumber,

                    CustomerEmail = order.Customer.Email,

                    DeliveryAddress = order.DeliveryAddress,

                    TotalPrice = order.TotalPrice,

                    Status = order.Status,

                    OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                    {
                        Id = op.Id,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        UnitPrice = op.UnitPrice,
                        ProductName = op.Product.Name

                    }).ToList()
                }).ToList();
        }

        public OrderDto GetById(int id)
        {
            return _context.Orders.Where(o => o.Id == id)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                .Select(order => new OrderDto
                {
                    Id = order.Id,

                    Reference = order.Reference,

                    Date = order.Date,

                    CustomerId = order.CustomerId,

                    CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}",

                    CustomerPhone = order.Customer.PhoneNumber,

                    CustomerEmail = order.Customer.Email,

                    DeliveryAddress = order.DeliveryAddress,

                    TotalPrice = order.TotalPrice,

                    Status = order.Status,

                    OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                    {
                        Id = op.Id,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        UnitPrice = op.UnitPrice,
                        ProductName = op.Product.Name

                    }).ToList()
                }).SingleOrDefault();
        }

        public async Task<OrderDto> GetByReference(string reference)
        {
            return await _context.Orders.Where(o => o.Reference == reference)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                .Select(order => new OrderDto
                {
                    Id = order.Id,

                    Reference = order.Reference,

                    Date = order.Date,

                    CustomerId = order.CustomerId,

                    CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}",

                    CustomerPhone = order.Customer.PhoneNumber,

                    CustomerEmail = order.Customer.Email,

                    DeliveryAddress = order.DeliveryAddress,

                    TotalPrice = order.TotalPrice,

                    Status = order.Status,

                    OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                    {
                        Id = op.Id,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        UnitPrice = op.UnitPrice,
                        ProductName = op.Product.Name

                    }).ToList()
                }).SingleOrDefaultAsync();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }


        //Payments...
        public async Task<List<PaymentDto>> GetAllCustomerPayment(int customerId)
        {
            return await _context.Orders.Where(o => o.Paid && o.CustomerId == customerId)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                .Select(payment => new PaymentDto 
                {
                    Reference = payment.Reference,
                    CustomerId = payment.CustomerId,
                    CustomerName = payment.Customer.FirstName + " " + payment.Customer.LastName,
                    OrderId = payment.Id,
                    Amount = payment.TotalPrice,
                    Date = payment.PaidAt,
                    OrderReference = payment.Reference
                }).ToListAsync();
        }

        public async Task<List<PaymentDto>> GetAllPayments()
        {
            return await _context.Orders.Where(o => o.Paid)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                .Select(payment => new PaymentDto
                {
                    Reference = payment.Reference,
                    CustomerId = payment.CustomerId,
                    CustomerName = payment.Customer.FirstName + " " + payment.Customer.LastName,
                    OrderId = payment.Id,
                    Amount = payment.TotalPrice,
                    Date = payment.PaidAt,
                    OrderReference = payment.Reference
                }).ToListAsync();
        }

        public async Task<PaymentDto> GetPaymentByReference(string reference)
        {
            return await _context.Orders.Where(o => o.Reference == reference)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                .Select(payment => new PaymentDto
                {
                    Reference = payment.Reference,
                    CustomerId = payment.CustomerId,
                    CustomerName = payment.Customer.FirstName + " " + payment.Customer.LastName,
                    OrderId = payment.Id,
                    Amount = payment.TotalPrice,
                    Date = payment.PaidAt,
                    OrderReference = payment.Reference
                }).SingleOrDefaultAsync();
        }

        public async Task<Order> FindByReference(string reference)
        {
            return await _context.Orders.Where(o => o.Reference == reference).Include(o => o.Customer)
                .SingleOrDefaultAsync();
        }
    }
}
