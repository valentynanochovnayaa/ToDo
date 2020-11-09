using Domain.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Service.Abstraction.Dxos;

namespace Services.Dxos
{
    public class ToDoUserDxo : IToDoUserDxo
    {
        public User Map(RegisterUserCommand command)
        {
            var user = new User
            {
                UserName  = command.Username,
                Email = command.Email,
                PasswordHash = command.Password,
            };
            return user;
        }
        
    }
}