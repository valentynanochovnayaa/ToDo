using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.Commands;
using Domain.Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Queries;
using MediatR;

namespace Data.Abstraction
{
    public interface IToDoItemRepository
    {
        Task<Result<Guid, ErrorsEnum>> CreateToDoItem(CreateToDoItemCommand createToDoItemCommand);
        Task<Result<Guid, ErrorsEnum>> UpdateToDoItem(UpdateToDoItemCommand updateToDoItemCommand);
        Task<Result<Unit, ErrorsEnum>> DeleteToDoItem(DeleteToDoItemCommand deleteToDoItemCommand);
        Task<Result<List<ToDoItemDto>,ErrorsEnum>> GetToDoItems(GetToDoItemsQuery getToDoItemsQuery);

    }
}