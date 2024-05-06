using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Respository.IRespository
{
    public interface IOrderManageRepository
    {
        Task<IEnumerable<Order>> GetUserOrders();
        Task<Order> Deliver(int IdOrder);
        Task<Order> CompleteOrder(int OrderId);
        Task<IEnumerable<OrderDetail>> OrderDetail(int OrderId);
    }
}
