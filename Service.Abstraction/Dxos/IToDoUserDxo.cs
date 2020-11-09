using Domain.Commands;
using Domain.Entities;

namespace Service.Abstraction.Dxos
{
    public interface IToDoUserDxo
    {
        public User Map(RegisterUserCommand command);
        
    }
}