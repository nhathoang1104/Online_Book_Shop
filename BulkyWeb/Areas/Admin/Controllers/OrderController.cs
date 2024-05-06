using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Respository;
using BulkyBook.DataAccess.Respository.IRespository;
using BulkyBook.Models.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static NuGet.Packaging.PackagingConstants;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly IOrderManageRepository _omr;
        private readonly ApplicationDbContext _db;
        public OrderController(IOrderManageRepository omr, ApplicationDbContext db)
        {
            _omr = omr;
            _db = db;
        }


        public async Task<IActionResult> GetOrder()
        {       
                var orders = await _omr.GetUserOrders();
                return View(orders);           
        }

        public async Task<IActionResult> Deliver(int orderId)
        {
            try
            {
                var deliveredOrder = await _omr.Deliver(orderId);
                TempData["success"] = "Order confirmed.";
                return RedirectToAction("GetOrder");
            }
            catch (Exception ex)
            {               
                TempData["error"] = "Invalid action.";
                return RedirectToAction("GetOrder");
            }
        }
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            try
            {
                var deliveredOrder = await _omr.CompleteOrder(orderId);
                TempData["success"] = "Delivery completed.";
                return RedirectToAction("GetOrder");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Invalid action.";
                return RedirectToAction("GetOrder");
            }
        }

        public async Task<IActionResult> Details(int orderId)
        {
            var orderdetails = await _omr.OrderDetail(orderId);
            return View(orderdetails);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Order> obj = _db.Orders.ToList();
            return Json(new { data = obj });

        }
        #endregion
    }
}
