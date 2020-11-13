using System;

namespace Domain.DTO
{
    public class ToDoItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public bool IsCompleted { get; set; }
    }
}