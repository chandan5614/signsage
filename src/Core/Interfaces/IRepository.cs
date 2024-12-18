using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Method to get an entity by its ID
        T GetById(int id);

        // Method to get all entities
        IEnumerable<T> GetAll();

        // Method to find entities based on a predicate
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        // Method to add a new entity
        void Add(T entity);

        // Method to update an existing entity
        void Update(T entity);

        // Method to delete an entity by its ID
        void Delete(int id);

        // Method to save changes to the database
        void Save();
    }
}
