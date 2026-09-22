using AhmadShop.DAL.Data;
using AhmadShop.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AhmadShop.DAL.Repositires
{
    public class GenericRepository<T> : IGenericRepositry<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public List<T> Get()
        {
            var entities = _context.Set<T>().ToList();
            return entities;
        }

        public T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}