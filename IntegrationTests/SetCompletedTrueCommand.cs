using System;

namespace IntegrationTests
{
    public class SetCompletedTrueCommand
    {
        public SetCompletedTrueCommand(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
    }
}