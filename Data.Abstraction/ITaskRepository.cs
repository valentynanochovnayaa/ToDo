using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Data
{
    public interface ITaskRepository
    {
        Task<Task> GetTask(int id);
        Task<ICollection<Task>> GetTasks();
    }
}