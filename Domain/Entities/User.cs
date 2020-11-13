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

        public Guid Id { get; set; }
        public virtual ICollection<ToDoItem> ToDoItems { get; set; }
    }
}
