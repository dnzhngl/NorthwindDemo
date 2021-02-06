using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>  // Bu bir IEntityRepository'dir.Hangi tabloyu verirsen onun reposu olacak.
        where TEntity : class, IEntity, new()    // class: referans tip, new(): newlenebilir
        where TContext : DbContext, new()  // DbContext:  TContext'in EfCore DbContext'ten inherit edilmiş olması lazım.
    {
        public void Add(TEntity entity)
        {   // IDisposable pattern implementation of C#
            using (TContext context = new TContext()) // using içerisine yazılan nesneler using bitince anında garbage collector tarafından silinir/ belleği hızlıca temizler. Daha performanslı olur.
            {
                var addedEntity = context.Entry(entity); // Referansı yakalar. Git veri kaynağından göndermiş olduğum nesneyi eşleştir.
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter); // filter => lambda exp.
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
                // filter : parametre olarak lambda expression gönderilir
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
