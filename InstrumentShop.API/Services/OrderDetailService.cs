using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using InstrumentShop.Shared.Models.DTOs;


namespace InstrumentShop.API.Services
{
    public class OrderDetailService
    {
        private readonly InstrumentShopDbContext _context;

        public OrderDetailService(InstrumentShopDbContext context)
        {
            _context = context;
        }

        public List<OrderDetailDto> GetOrderDetailsByOrderId(int orderId)
        {
            // Sử dụng LINQ to Entities để tối ưu hóa truy vấn
            var query = from od in _context.OrderDetails
                        where od.OrderId == orderId
                        select new OrderDetailDto
                        {
                            OrderId = od.OrderId,
                            InstrumentId = od.InstrumentId,
                            InstrumentName = od.Instrument.Name,
                            Quantity = od.Quantity,
                            Price = od.Price
                        };

            return query.ToList();
        }

        public OrderDetailDto GetOrderDetailById(int orderId, int instrumentId)
        {
            // Sử dụng LINQ to Entities để tối ưu hóa truy vấn
            var query = from od in _context.OrderDetails
                        where od.OrderId == orderId && od.InstrumentId == instrumentId
                        select new OrderDetailDto
                        {
                            OrderId = od.OrderId,
                            InstrumentId = od.InstrumentId,
                            InstrumentName = od.Instrument.Name,
                            Quantity = od.Quantity,
                            Price = od.Price
                        };

            return query.FirstOrDefault();
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();
        }

        public void DeleteOrderDetail(int orderId, int instrumentId)
        {
            var orderDetail = _context.OrderDetails.Find(orderId, instrumentId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                _context.SaveChanges();
            }
        }
    }
}