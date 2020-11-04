using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Data.Abstraction
{
    public interface ITaskRepository
    {
        Task<ToDoItem> GetTask(int id);
        Task<ICollection<ToDoItem>> GetTasks();
    }
}