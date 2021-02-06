using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{

    public class InMemoryCategoryDal : ICategoryDal
    {
        List<Category> _categories;
        public InMemoryCategoryDal()
        {
            _categories = new List<Category>
            {
                new Category{ CategoryId=1, CategoryName="Züccaciye" },
                new Category{ CategoryId=2, CategoryName="Elektronik" },
            };
        }

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public void Delete(Category category)
        {
            var categoryToDelete = _categories.SingleOrDefault(c => c.CategoryId == category.CategoryId);
            _categories.Remove(categoryToDelete);
        }

        //public Category Get(int categoryId)
        //{
        //    var category = _categories.SingleOrDefault(c => c.CategoryId == categoryId);
        //    return category;
        //}

        public Category Get(Expression<Func<Category, bool>> filter)
        {
            var query = filter.Compile();
            return (Category)_categories.SingleOrDefault(query.Invoke);
        }

        //public List<Category> GetAll()
        //{
        //    return _categories;
        //}

        public List<Category> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            if (filter == null)
            {
                return _categories;
            }
            else
            {
                var query = filter.Compile();
                return _categories.Where(query.Invoke).ToList();
            }
        }

        public void Update(Category category)
        {
            var categoryToUpdate = _categories.SingleOrDefault(c => c.CategoryId == category.CategoryId);
            categoryToUpdate.CategoryName = category.CategoryName;
        }
    }
}
