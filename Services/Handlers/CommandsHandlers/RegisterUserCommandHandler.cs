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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Unit, ErrorsEnum>>
    {
        private readonly IAuthRepository _repo;
        public RegisterUserCommandHandler(IAuthRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<Unit, ErrorsEnum>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var createdUser = await _repo.Register(request);
            return Unit.Value;
        }
        
    }
}