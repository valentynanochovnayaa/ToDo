using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Commands;
using Domain.Common;
using MediatR;

namespace Services.Handlers.CommandsHandlers
{
    public class SetCompletedTrueCommandHandler : IRequestHandler<SetCompletedTrueCommand, Result<Guid, ErrorsEnum>>
    {
        private readonly IToDoItemRepository _repo;

        public SetCompletedTrueCommandHandler(IToDoItemRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<Guid, ErrorsEnum>> Handle(SetCompletedTrueCommand request, CancellationToken cancellationToken)
        {
            return await _repo.SetCompletedTrue(request);
        }
    }
}