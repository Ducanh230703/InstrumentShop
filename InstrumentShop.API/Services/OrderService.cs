using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstrumentShop.API.Services
{
    public class OrderService
    {
        private readonly InstrumentShopDbContext _context;

        public OrderService(InstrumentShopDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Find(id);
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.OrderStatus = status;
                _context.SaveChanges();
            }
        }

        public void AddOrderWithDetails(Order order, List<OrderDetail> orderDetails)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    foreach (var detail in orderDetails)
                    {
                        detail.OrderId = order.OrderId;
                        _context.OrderDetails.Add(detail);
                    }
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error adding order with details: " + ex.Message);
                }
            }
        }
        // Trong OrderService
        public void CalculateTotalAmount(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return; // Hoặc throw exception nếu cần
            }

            order.TotalAmount = _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Sum(od => od.Price);

            _context.SaveChanges();
        }
    }
}