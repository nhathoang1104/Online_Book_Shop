using BulkyBook.DataAccess.Respository.IRespository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller 
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(int productId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(productId, qty);
            if (redirect == 0)
            {
                TempData["success"] = "Added product to cart successfully."; ;
                return Ok(cartCount);
            }
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int productId)
        {
            var cartCount = await _cartRepo.RemoveItem(productId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            try
            {
                var cart = await _cartRepo.GetUserCart();
                return View(cart);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Please log in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        public async Task<IActionResult> Checkout(string fullName, string phoneNumber, string address)
        {
            bool isCheckedOut = await _cartRepo.DoCheckout(fullName, phoneNumber, address);
            if (!isCheckedOut)
            {
                TempData["error"] = "You need to fill in all information."; ;
                return RedirectToAction("GetUserCart");
            }
            else
            {
                TempData["success"] = "Order Success"; ;
                
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
