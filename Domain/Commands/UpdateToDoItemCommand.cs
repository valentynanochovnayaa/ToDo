using System;
using CSharpFunctionalExtensions;
using Domain.Common;
using MediatR;

namespace Domain.Commands
{
    public class UpdateToDoItemCommand : IRequest<Result<Guid, ErrorsEnum>>
    {
        public UpdateToDoItemCommand()
        {
            
        }
        public UpdateToDoItemCommand(string name, string description, DateTimeOffset deadLine, bool isCompleted)
        {
            Name = name;
            Description = description;
            Deadline = deadLine;
            IsCompleted = isCompleted;
        }

        public UpdateToDoItemCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
        public UpdateToDoItemCommand(Guid id, string name, string description, DateTimeOffset deadLine, bool isCompleted, Guid userId)
        {
            Id = id;
            Name = name;
            Description = description;
            Deadline = deadLine;
            IsCompleted = isCompleted;
            UserId = userId;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
    }
}