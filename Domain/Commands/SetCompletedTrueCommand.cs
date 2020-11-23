using System;
using CSharpFunctionalExtensions;
using Domain.Common;
using MediatR;

namespace Domain.Commands
{
    public class SetCompletedTrueCommand : IRequest<Result<Guid, ErrorsEnum>>
    {
        public SetCompletedTrueCommand()
        {
            
        }
        public SetCompletedTrueCommand(Guid toDoItemId, Guid userId)
        {
            ToDoItemId = toDoItemId;
            UserId = userId;
        }
        public Guid ToDoItemId { get; set; }
        public Guid UserId { get; set; }
    }
}