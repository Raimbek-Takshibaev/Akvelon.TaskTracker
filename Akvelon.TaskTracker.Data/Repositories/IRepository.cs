using Akvelon.TaskTracker.Data.Helpers;
using Akvelon.TaskTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akvelon.TaskTracker.Data.Repositories
{
    // repository pattern implementation
    public interface IRepository<T> : IDisposable where T : Entity
    {
        IEnumerable<T> GetAll(); // get all elements
        T First(int id); // get element by id
        void Create(T item); // create new element
        void Update(T item); // update element
        void Delete(int id); // delete element by id
        System.Threading.Tasks.Task SaveChanges();  // update db
    }
}
