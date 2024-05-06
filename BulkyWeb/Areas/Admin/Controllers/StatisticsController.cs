using BulkyBook.DataAccess.Data;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StatisticsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStats(string fromDate, string toDate)
        {
            var query = from o in _db.Orders
                        join od in _db.OrderDetails
                        on o.Id equals od.OrderId
                        where (o.OrderStatusId == 3)
                        select new
                        {
                            CreatedDate = o.CreateDate,
                            Quantity= od.Quantity,
                            UnitePrice=od.UnitPrice
                        };

            if(!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", null);
                query=query.Where(d => d.CreatedDate >= startDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", null);
                query = query.Where(d => d.CreatedDate <= endDate);
            }

            var result = query.GroupBy(x => x.CreatedDate.Date).Select(x => new
            {
                Date = x.Key,
                TotalBuy = x.Sum(y => y.Quantity * y.UnitePrice),
                NumberOfOrder = x.Count()
            }).Select(x => new
            {
                Date=x.Date,
                TotalBuy=x.TotalBuy,
                NoB=x.NumberOfOrder
            });
            return Json(new {Data=result});
        }
    }
}
