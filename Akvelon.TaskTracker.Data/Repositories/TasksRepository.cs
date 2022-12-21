using Akvelon.TaskTracker.Data.Helpers;
using EntityTask = Akvelon.TaskTracker.Data.Models.Task;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akvelon.TaskTracker.Data.Repositories
{
    public class TasksRepository : IRepository<EntityTask>
    {
        TaskTrackerDbContext _context;
        private bool disposedValue;

        public TasksRepository(TaskTrackerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EntityTask> GetAll() // get all task table
        {
            return _context.Tasks.AsNoTracking().Include(task => task.Project);
        }
        public EntityTask First(int id) // get task by id 
        {
            return _context.Tasks.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }
        public void Create(EntityTask item) // create task
        {
            _context.Tasks.Add(item);
        }
        public void Update(EntityTask item) // update task
        {
            _context.Tasks.Update(item);
        }
        public void Delete(int id) // delete task by id
        {
            _context.Tasks.Remove(First(id));
        } 
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        // idisposable implementation
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TasksRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
