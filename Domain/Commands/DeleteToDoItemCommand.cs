using System;
using CSharpFunctionalExtensions;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Commands
{
    public class DeleteToDoItemCommand : IRequest<Result<Unit, ErrorsEnum>>
    {
        public DeleteToDoItemCommand()
        {
            
        }

        public DeleteToDoItemCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}