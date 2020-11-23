using System;
using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.DTO;
using MediatR;

namespace Domain.Queries
{
    public class GetToDoItemQuery : IRequest<Result<ToDoItemDto, ErrorsEnum>>
    {
        public GetToDoItemQuery(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}