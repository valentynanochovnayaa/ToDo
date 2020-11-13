using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Commands;
using Domain.Common;
using MediatR;

namespace Services.Handlers.CommandsHandlers
{
    public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, Result<Unit, ErrorsEnum>>
    {
        private readonly IToDoItemRepository _repo;
        public DeleteToDoItemCommandHandler(IToDoItemRepository repo)
        {
            _repo = repo;
        }
        public Task<Result<Unit, ErrorsEnum>> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            return _repo.DeleteToDoItem(request);
        }
    }
}