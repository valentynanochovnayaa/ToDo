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
    public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, Result<Guid, ErrorsEnum>>
    {
        private readonly IToDoItemRepository _repo;

        public UpdateToDoItemCommandHandler(IToDoItemRepository repo)
        {
            _repo = repo;
        }
        public Task<Result<Guid, ErrorsEnum>> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            return _repo.UpdateToDoItem(request);
        }
    }
}