using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Commands;
using Domain.Common;
using Domain.DTO;
using MediatR;

namespace Services.Handlers.QueriesHandlers
{
    public class IfMissedToDoItemQueryHandler : IRequestHandler<IfMissedToDoItemQuery, Result<ToDoItemDto, ErrorsEnum>>
    {
        private readonly IToDoItemRepository _repo;

        public IfMissedToDoItemQueryHandler(IToDoItemRepository repo)
        {
            _repo = repo;
        }
        public Task<Result<ToDoItemDto, ErrorsEnum>> Handle(IfMissedToDoItemQuery request, CancellationToken cancellationToken)
        {
            return _repo.IfMissedToDoItem(request);
        }
    }
}