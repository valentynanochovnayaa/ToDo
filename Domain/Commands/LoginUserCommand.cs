using CSharpFunctionalExtensions;
using Domain.Common;
using MediatR;
using ToDo.DTO;

namespace Domain.Commands
{
    public class LoginUserCommand : IRequest<Result<TokenDto,ErrorsEnum >>
    {
        public LoginUserCommand()
        {
            
        }

        public LoginUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}