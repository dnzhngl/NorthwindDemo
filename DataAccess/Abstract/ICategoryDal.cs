using Entiities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICategoryDal
    {
        List<Category> GetAll();
        Category Get(int categoryId);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
