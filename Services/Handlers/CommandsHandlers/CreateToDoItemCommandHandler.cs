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
    public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Result<Guid, ErrorsEnum>>
    {
        private readonly IToDoItemRepository _repo;
        public CreateToDoItemCommandHandler(IToDoItemRepository repo)
        {
            _repo = repo;
        }
        public Task<Result<Guid, ErrorsEnum>> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            return _repo.CreateToDoItem(request);
            
        }
    }
}