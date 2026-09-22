using System;
using System.Collections.Generic;
using System.Text;

namespace AhmadShop.DAL.Repositires
{
    public interface IGenericRepositry<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Remove(T entity);
            List<T> Get();
        T Find(int id);

    }
}
