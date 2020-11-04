using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data
{
    public class TaskRepository: ITaskRepository
    {
        private readonly DataContext _context;
        public TaskRepository(DataContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task<Task> GetTask(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }

        public System.Threading.Tasks.Task<ICollection<Task>> GetTasks()
        {
            throw new System.NotImplementedException();
        }

        
    }
}