using System.Threading.Tasks;
using System.Web.Providers.Entities;
using Domain.Entities;
using ToDo.Entities;

namespace ToDo.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
    }
}