using Shop.Core.Logic;
using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAcces.Sql
{
    public class SQlRepository<T> : IRepository<T> where T:BaseEntity
    {
        internal MyContext DataContext;
        internal DbSet<T> dbset;

        public SQlRepository(MyContext DataContext)
        {
            this.DataContext = DataContext;
            dbset = DataContext.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbset;
        }

        public void Delete(int id)
        {
            T t = FindById(id);
            if (DataContext.Entry(t).State == EntityState.Detached)
            {
                dbset.Attach(t);
            }
        }

        public T FindById(int id)
        {
            return dbset.Find(id);
        }

        public void Insert(T t)
        {
            dbset.Add(t);
        }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }

        public void Update(T t)
        {
            dbset.Attach(t);
            DataContext.Entry(t).State = EntityState.Modified;
        }
    }

}
