using System;
using Domain.DTO;
using Domain.Entities;
using Service.Abstraction.Dxos;

namespace Services.Dxos
{
    public class ToDoItemDxo : IToDoItemDxo
    {
        public ToDoItemDto Map(ToDoItem entity)
        {
            var item = new ToDoItemDto
            {
                Name = entity.Name,
                Description = entity.Description,
                Deadline = entity.Deadline,
                IsCompleted = entity.IsCompleted,
                IsMissed = true && (entity.Deadline < DateTimeOffset.Now)
            };
            return item;
        }
    }
}