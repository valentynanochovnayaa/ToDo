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
        public string Username { get; set; }
        public string Password { get; set; }
    }
}