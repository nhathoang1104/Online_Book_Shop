using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class ProductViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public string PriceSortOrder { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }  
}
