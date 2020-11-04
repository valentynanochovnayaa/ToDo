using System.Threading.Tasks;
using Domain.Entities;


namespace Data.Abstraction
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
    }
}