using System.Collections.Generic;
using Data.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class TaskRepository: ITaskRepository
    {
        private readonly DataContext _context;
        public TaskRepository(DataContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task<ToDoItem> GetTask(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }

        public System.Threading.Tasks.Task<ICollection<ToDoItem>> GetTasks()
        {
            throw new System.NotImplementedException();
        }

        
    }
}