using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
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
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Add(Category category)
        {
            var result = _categoryDal.Get(c => c.CategoryName == category.CategoryName);
            if (result == null)
            {
                _categoryDal.Add(category);
                return new SuccessResult(Messages.Categories.Add(category.CategoryName));
            }
            return new ErrorResult(Messages.Categories.Exists(category.CategoryName));
        }
        public IResult Delete(Category category)
        {
            var result = _categoryDal.Get(c => c.CategoryId == category.CategoryId);
            if (result != null)
            {
                _categoryDal.Delete(category);
                return new SuccessResult(Messages.Categories.Delete(category.CategoryName));
            }
            return new ErrorResult(Messages.NotFound);
        }
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Update(Category category)
        {
            var result = _categoryDal.Get(c => c.CategoryId == category.CategoryId);
            if (result != null)
            {
                _categoryDal.Update(category);
                return new SuccessResult(Messages.Categories.Update(category.CategoryName));
            }
            return new ErrorResult(Messages.NotFound);
        }
       
        public IDataResult<Category> GetById(int categoryId) // SELECT * FROM Categories WHERE CategoryID = categoryId;
        {
            var result = _categoryDal.Get(c => c.CategoryId == categoryId);
            if (result != null)
            {
                return new SuccessDataResult<Category>(result);
            }
            return new ErrorDataResult<Category>(Messages.NotFound);
            
        }
        public IDataResult<List<Category>> GetAll()
        {
            var result = _categoryDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Category>>(result);
            }
            return new ErrorDataResult<List<Category>>(Messages.NotFound);
        }

    }
}
