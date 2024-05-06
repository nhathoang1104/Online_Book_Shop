
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Respository.IRespository;
using BulkyBook.Models.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public IActionResult Index(string term="", string orderBy="",int currentPage=1)
        {   
            term =string.IsNullOrEmpty(term)?"":term.ToLower();
            var proData = new ProductViewModel();
            proData.PriceSortOrder = string.IsNullOrEmpty(orderBy) ? "price_desc" : "";
            var products = (from pro in _db.Products
                            where term=="" || pro.Title.ToLower().Contains(term)
                            select new Product
                            {
                                Id = pro.Id,
                                Title = pro.Title,
                                Description = pro.Description,
                                ISBN = pro.ISBN,
                                Author=pro.Author,
                                ListPrice = pro.ListPrice,
                                Price=pro.Price,
                                Price50 = pro.Price50,
                                Price100 = pro.Price100,
                                CategoryId = pro.CategoryId,
                                ImageUrl = pro.ImageUrl,
                                IsActive = pro.IsActive,
                            });
            switch (orderBy)
            {
                case "price_desc":
                    products = products.OrderByDescending(u => u.ListPrice);
                    break;
                default:
                    products = products.OrderBy(u => u.ListPrice);
                    break;
            }

            var totalRecords = products.Count();
            int pageSize = 8;
            int totalPages = (int) Math.Ceiling(totalRecords /(double) pageSize);
            products = products.Skip((currentPage - 1) * pageSize).Take(pageSize);

            proData.Products = products;
            proData.CurrentPage = currentPage;
            proData.PageSize = pageSize;
            proData.Term = term;
            proData.OrderBy = orderBy;
            proData.TotalPages = totalPages;

            return View(proData);

            //IEnumerable<Product> productList = _db.Products.ToList();
            //return View(productList);
        }
        public IActionResult Details(int productId)
        {
            Product product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category");
            return View(product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}