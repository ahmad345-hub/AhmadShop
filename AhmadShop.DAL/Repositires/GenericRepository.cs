using AhmadShop.DAL.Data;
using AhmadShop.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AhmadShop.DAL.Repositires
{
    public class CategoryRepository<T> where T : class
    {
        ApplicationDpContext context = new ApplicationDpContext();

        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public List<T> Get()
        {
            var entities = context.Set<T>().ToList();
            return entities;
        }

        public T Find(int id)
        {
            return context.Set<T>().Find(id);
        }
    }
}