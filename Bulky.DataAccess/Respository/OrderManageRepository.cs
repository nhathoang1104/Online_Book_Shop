using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Respository.IRespository;
using BulkyBook.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Respository
{
    public class OrderManageRepository : IOrderManageRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderManageRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Order>> GetUserOrders()
        {
            var orders = await _db.Orders
                            .Include(x => x.OrderStatus)
                            .Include(x => x.OrderDetail)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Category)
                            .ToListAsync();
            return orders;
        }

        public async Task<Order> Deliver (int OrderId)
        {
            var order= await _db.Orders.FirstOrDefaultAsync(x => x.Id== OrderId);
            if (order == null) 
            { 
                throw new Exception("Đơn hàng không tồn tại."); 
            }
            if (order.OrderStatusId != 1)
            {               
                throw new Exception("Đơn hàng đã được giao hàng trước đó.");
            }
            if (order.OrderStatusId == 1)
            {
                order.OrderStatusId = 2;
            }

            await _db.SaveChangesAsync(); 
            return order;
        }
        public async Task<Order> CompleteOrder(int OrderId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(x => x.Id == OrderId);
            if (order == null)
            {
                throw new Exception("Đơn hàng không tồn tại.");
            }
            if (order.OrderStatusId != 2)
            {
                throw new Exception("Đơn hàng chưa được vận chuyển.");
            }
            if (order.OrderStatusId == 2)
            {
                order.OrderStatusId = 3;
            }

            await _db.SaveChangesAsync();
            return order;
        }
        public async Task<IEnumerable<OrderDetail>> OrderDetail(int OrderId) 
        {
            var orderdetail = await _db.OrderDetails
                                  .Include(x => x.Product)
                                  .Include(x => x.Order)
                                  .Where(x => x.OrderId == OrderId)
                                  .ToListAsync();
            return orderdetail;
        }
    }
}
