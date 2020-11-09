using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Commands;
using Domain.Common;
using MediatR;

namespace Services.Handlers.CommandsHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<Unit, Error>>
    {
        private readonly IAuthRepository _repo;
        public LoginUserCommandHandler(IAuthRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<Unit, Error>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.Login(request);
            return Unit.Value;

        }
    }
    
}