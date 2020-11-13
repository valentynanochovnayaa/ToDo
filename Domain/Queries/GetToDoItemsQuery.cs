using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.DTO;
using MediatR;

namespace Domain.Queries
{
    public class GetToDoItemsQuery : IRequest<Result<List<ToDoItemDto>, ErrorsEnum>>
    {
        public GetToDoItemsQuery()
        {
            
        }

        public GetToDoItemsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}