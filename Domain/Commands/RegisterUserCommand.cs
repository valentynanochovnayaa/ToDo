using CSharpFunctionalExtensions;
using Domain.Common;
using MediatR;
using ToDo.DTO;

namespace Domain.Commands
{
    public class RegisterUserCommand : IRequest<Result<Unit, ErrorsEnum>>
    {
        public RegisterUserCommand(string username, string password, string email)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        public RegisterUserCommand()
        {
            
        }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}