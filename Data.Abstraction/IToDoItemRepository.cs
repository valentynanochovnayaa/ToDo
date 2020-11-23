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
        Task<Result<ToDoItemDto, ErrorsEnum>> GetToDoItem(GetToDoItemQuery getToDoItemQuery);
        Task<Result<List<ToDoItemDto>,ErrorsEnum>> GetToDoItems(GetToDoItemsQuery getToDoItemsQuery);
        Task<Result<Guid, ErrorsEnum>> SetCompletedTrue(SetCompletedTrueCommand setCompletedTrueCommand);
        Task<Result<ToDoItemDto, ErrorsEnum>> IfMissedToDoItem(IfMissedToDoItemQuery ifMissedToDoItemCommand);

    }
}