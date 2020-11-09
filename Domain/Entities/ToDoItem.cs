using System;

namespace Domain.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
               
    }
}
