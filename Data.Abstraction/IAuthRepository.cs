using System.Threading.Tasks;
using Domain.Commands;
using Domain.Entities;
using ToDo.DTO;


namespace Data.Abstraction
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, RegisterUserCommand request, string password);
        Task<User> Login(string username, string password);
    }
}