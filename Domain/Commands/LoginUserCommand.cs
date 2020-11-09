using CSharpFunctionalExtensions;
using Domain.Common;
using MediatR;

namespace Domain.Commands
{
    public class LoginUserCommand : IRequest<Result<Unit, Error>>
    {
        public LoginUserCommand()
        {
            
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}