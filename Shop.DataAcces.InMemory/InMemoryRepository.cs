﻿using Shop.Core.Logic;
using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAcces.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {

            className = typeof(T).Name;

            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }


        }
        public void SaveChanges()
        {
            cache[className] = items;
        }
        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T prodToUpdate = items.Find(prod => prod.Id == t.Id);
            if (prodToUpdate != null)
            {
                prodToUpdate = t;

            }
            else
            {
                throw new Exception("Product not find");
            }
        }

        public T FindById(int id)
        {
            T t = items.Find(prod => prod.Id == id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception("Product not find");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
        public void Delete(int id)
        {
            T prodToDelete = items.Find(p => p.Id == id);
            if (prodToDelete != null)
            {
                items.Remove(prodToDelete);
            }
            else
            {
                throw new Exception(className + " not find");
            }
        }

    }
}

