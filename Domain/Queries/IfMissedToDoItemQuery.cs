using System;
using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.DTO;
using MediatR;

namespace Domain.Commands
{
    public class IfMissedToDoItemQuery : IRequest<Result<ToDoItemDto, ErrorsEnum>>
    {
        public IfMissedToDoItemQuery()
        {
            
        }
        public IfMissedToDoItemQuery(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}