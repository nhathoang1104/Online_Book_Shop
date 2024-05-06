﻿using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Respository.IRespository
{
    public interface ICategoryRespository : IRespository<Category>
    {
        void Update(Category obj);
        
    }
}
