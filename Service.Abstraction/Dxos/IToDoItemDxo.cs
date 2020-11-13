using Domain.DTO;
using Domain.Entities;

namespace Service.Abstraction.Dxos
{
    public interface IToDoItemDxo
    {
        public ToDoItemDto Map(ToDoItem entity);
    }
}