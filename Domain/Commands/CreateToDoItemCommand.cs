using System;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.Entities;
using MediatR;
using ToDo.DTO;

namespace Domain.Commands
{
    public class CreateToDoItemCommand : IRequest<Result<Guid, ErrorsEnum>>
    {
        public CreateToDoItemCommand(string name, string description, DateTimeOffset deadLine, bool isCompleted)
        {
            Name = name;
            Description = description;
            Deadline = deadLine;
        }
        public CreateToDoItemCommand(string name, string description, DateTimeOffset deadLine, bool isCompleted, Guid userId)
        {
            Name = name;
            Description = description;
            Deadline = deadLine;
            UserId = userId;
        }

        public CreateToDoItemCommand()
        {
            
        }
        public string Name { get; set;}
        public string Description { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public bool IsCompleted { get; set; } = false;
        public Guid UserId { get; set; }
        
    }
}