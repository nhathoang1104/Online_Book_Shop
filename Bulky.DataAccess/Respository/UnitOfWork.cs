using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Respository.IRespository;
using BulkyBook.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Respository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRespository Category { get; private set; }
        public IProductRespository Product { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category= new CategoryRespository(_db);
            Product= new ProductRespository(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task<Product?> ActiveProduct(int proId)
        {
            var pro = await _db.Products.FirstOrDefaultAsync(x => x.Id == proId);
            if (pro != null)
            {
                pro.IsActive = !pro.IsActive; // Chuyển đổi giá trị IsActive
                await _db.SaveChangesAsync();
            }
            return pro;
        }

    }
}
