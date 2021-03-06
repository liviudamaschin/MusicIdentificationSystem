﻿using MusicIdentificationSystem.EF.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal Db context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(Db context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            TEntity insertedEntity=dbSet.Add(entity);
            return insertedEntity;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Save(TEntity entityToSave)
        {
           // if (context.Entry(entityToSave).Property("Id").CurrentValue )
        }

        public virtual bool AlreadyExists(string propertyOrField, string value)
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity));
            Expression boby = Expression.Equal(Expression.PropertyOrField(param, propertyOrField),
                  Expression.Constant(value, typeof(string)));
            Expression<Func<TEntity, bool>> filter = Expression.Lambda<Func<TEntity, bool>>(boby, param);

            var entities = Get(filter);

            if (entities != null && entities.Count() > 0)
                return true;
            else
                return false;
        }
    }
}
