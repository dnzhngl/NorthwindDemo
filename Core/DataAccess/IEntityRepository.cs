using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{   // Generic Repository Design Pattern 
    public interface IEntityRepository<T> where T : class, IEntity, new() // Generic Constraint - 
    // T tipinde ne gelirse o tipte çalışacak. Ancak T bir referans tip (class) olmaalı ve ya IEntity yada IEntity'den implemente olan bir tipte olmalı. 
    // new() : newlenebilir olmalı (IEntity interface olduğundan newlenemez.)
    {
        List<T> GetAll(Expression<Func<T,bool>> filter = null);   // Expression filtre vermemizi sağlayan yapı 
        T Get(Expression<Func<T, bool>> filter );
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
