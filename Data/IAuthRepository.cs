using System.Threading.Tasks;
using ToDo.DTO;
using ToDo.Models;

namespace ToDo.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
    }
}