using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        // Bağımlılığı Constructor injection ile yapıyoruz.
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }
        public void Delete(Category category)
        {
            _categoryDal.Delete(category);
        }
        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
       
        public Category GetById(int categoryId) // SELECT * FROM Categories WHERE CategoryID = categoryId;
        {
            return _categoryDal.Get(c => c.CategoryId == categoryId);
        }
        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

    }
}
