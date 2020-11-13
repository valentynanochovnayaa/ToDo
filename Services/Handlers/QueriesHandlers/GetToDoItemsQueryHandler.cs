using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Common;
using Domain.DTO;
using Domain.Queries;
using MediatR;

namespace Services.Handlers.QueriesHandlers
{
    public class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, Result<List<ToDoItemDto>,ErrorsEnum>>
    {
        private readonly IToDoItemRepository _repo;
        public GetToDoItemsQueryHandler(IToDoItemRepository repo)
        {
            _repo = repo;
        }
        public Task<Result<List<ToDoItemDto>, ErrorsEnum>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetToDoItems(request);
        }
    }
}