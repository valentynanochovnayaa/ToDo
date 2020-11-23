using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Commands;
using Domain.Common;
using MediatR;
using ToDo.DTO;

namespace Services.Handlers.CommandsHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<TokenDto, ErrorsEnum>>
    {
        private readonly IAuthRepository _repo;

        public LoginUserCommandHandler(IAuthRepository repo)
        {
            _repo = repo;
        }

        public Task<Result<TokenDto, ErrorsEnum>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return _repo.Login(request);
        }
    }
}