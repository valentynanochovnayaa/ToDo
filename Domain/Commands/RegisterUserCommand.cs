using CSharpFunctionalExtensions;
using Domain.Common;
using MediatR;
using ToDo.DTO;

namespace Domain.Commands
{
    public class RegisterUserCommand : IRequest<Result<Unit, Error>>
    {
        
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}