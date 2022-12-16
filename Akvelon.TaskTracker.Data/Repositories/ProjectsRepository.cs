using Akvelon.TaskTracker.Data.Helpers;
using Akvelon.TaskTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akvelon.TaskTracker.Data.Repositories
{
    public class ProjectsRepository : IRepository<Project>
    {
        TaskTrackerDbContext _context;
        private bool disposedValue;

        public ProjectsRepository(TaskTrackerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.AsNoTracking();
        }
        public Project First(int id) // get project by id 
        {
            return _context.Projects.AsNoTracking().FirstOrDefault(p => p.Id == id);
        } 
        public void Create(Project item) // create project
        {
            _context.Projects.Add(item);
        }
        public void Update(Project item) // update project
        {
            _context.Projects.Update(item);
        }
        public void Delete(int id) // delete project by id
        {
            _context.Projects.Remove(First(id));
        } 
        public async System.Threading.Tasks.Task SaveChanges() // save db
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
        // ~ProjectsRepository()
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
