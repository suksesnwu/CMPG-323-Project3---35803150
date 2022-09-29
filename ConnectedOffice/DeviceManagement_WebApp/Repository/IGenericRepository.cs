using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(Guid? id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        //update entity
        void Update(T entity);

        //save changes after update/delete/add
        Task SaveChanges();

        //check whether the item exist
        bool Exists(Guid? id);
    }
}
