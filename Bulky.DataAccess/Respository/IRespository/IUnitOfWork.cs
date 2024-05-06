using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Respository.IRespository
{
    public interface IUnitOfWork
    {
        ICategoryRespository Category { get; }
        IProductRespository Product { get; }
        void Save();
        Task<Product> ActiveProduct(int proId);
    }
}
