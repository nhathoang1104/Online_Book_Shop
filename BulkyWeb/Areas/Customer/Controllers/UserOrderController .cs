using BulkyBook.DataAccess.Respository.IRespository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRepository _userOrderRepo;

        public UserOrderController(IUserOrderRepository userOrderRepo)
        {
            _userOrderRepo = userOrderRepo;
        }
        public async Task<IActionResult> UserOrders()
        {
            try
            {
                var orders = await _userOrderRepo.UserOrders();
                return View(orders);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Please log in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> DeleteOrder(int OrderId) 
        {
            try
            {
                var order = await _userOrderRepo.DeleteOrder(OrderId);
                TempData["success"] = "You have canceled the order";
                return RedirectToAction("UserOrders");
            }
            catch 
            {
                TempData["error"] = "Order are being shipped";
                return RedirectToAction("UserOrders");
            }
        }
    }
}