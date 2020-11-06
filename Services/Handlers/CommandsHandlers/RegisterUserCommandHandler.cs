using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Commands;
using Domain.Common;
using Domain.Entities;
using MediatR;
using ToDo.DTO;

namespace Services.Handlers.CommandsHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Unit, Error>>
    {
        private readonly IAuthRepository _repo;
        public RegisterUserCommandHandler(IAuthRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<Unit, Error>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User();
            var createdUser = await _repo.Register(user, request, request.Password);
            return Unit.Value;
        }
        
    }
}