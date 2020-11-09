using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            
        }
        public ICollection<ToDoItem> ToDoItems { get; set; }
    }
}
