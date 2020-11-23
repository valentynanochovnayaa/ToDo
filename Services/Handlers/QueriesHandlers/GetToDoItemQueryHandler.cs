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
    public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, Result<ToDoItemDto, ErrorsEnum>>
    {
        private readonly IToDoItemRepository _repo;

        public GetToDoItemQueryHandler(IToDoItemRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<ToDoItemDto, ErrorsEnum>> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetToDoItem(request);
        }
    }
}