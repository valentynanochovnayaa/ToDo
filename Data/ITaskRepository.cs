using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = ToDo.Models.Task;

namespace ToDo.Data
{
    public interface ITaskRepository
    {
        Task<Task> GetTask(int id);
        Task<ICollection<Task>> GetTasks();
    }
}