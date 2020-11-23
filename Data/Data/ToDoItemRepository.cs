using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Commands;
using Domain.Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Service.Abstraction.Dxos;

namespace Data.Data
{
    public class ToDoItemRepository: IToDoItemRepository
    {
        private readonly DataContext _context;
        private readonly IToDoItemDxo _dxo;
        public ToDoItemRepository(DataContext context, IToDoItemDxo dxo)
        {
            _dxo = dxo;
            _context = context;
        }
        
        public async Task<Result<Guid, ErrorsEnum>> CreateToDoItem(CreateToDoItemCommand createToDoItemCommand)
        {
            var toDoItem = new ToDoItem
            {
                Name = createToDoItemCommand.Name,
                Description = createToDoItemCommand.Description,
                Deadline = createToDoItemCommand.Deadline,
                IsCompleted = false,
                UserId = createToDoItemCommand.UserId,
            };
            await _context.ToDoItems.AddAsync(toDoItem);
            await _context.SaveChangesAsync();
            return toDoItem.Id;
        }

        public async Task<Result<Guid, ErrorsEnum>> UpdateToDoItem(UpdateToDoItemCommand updateToDoItemCommand)
        { 
            var item = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(
                _context.ToDoItems, i => i.Id == updateToDoItemCommand.Id);
            
            item.Name = updateToDoItemCommand.Name;
            item.Description = updateToDoItemCommand.Description;
            item.Deadline = updateToDoItemCommand.Deadline;
            item.IsCompleted = updateToDoItemCommand.IsCompleted;
            item.UserId = updateToDoItemCommand.UserId;
            
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<Result<Unit, ErrorsEnum>> DeleteToDoItem(DeleteToDoItemCommand deleteToDoItemCommand)
        {
            var item = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(
                _context.ToDoItems, i => i.Id == deleteToDoItemCommand.Id);
            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Result<List<ToDoItemDto>, ErrorsEnum>> GetToDoItems(GetToDoItemsQuery getToDoItemsQuery)
        {
            var dbitem = await _context.ToDoItems
                .Where(i => i.UserId == getToDoItemsQuery.UserId)
                .Select(x => _dxo.Map(x)).ToListAsync();
            return dbitem;
        }

        public async Task<Result<ToDoItemDto, ErrorsEnum>> GetToDoItem(GetToDoItemQuery getToDoItemQuery)
        {
            var dbitem = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions
                .FirstOrDefaultAsync(_context.ToDoItems, i => i.Id == getToDoItemQuery.Id);
            return _dxo.Map(dbitem);
        }
        
        public async Task<Result<Guid, ErrorsEnum>> SetCompletedTrue(SetCompletedTrueCommand setCompletedTrueCommand)
        {
            var item = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions
                .FirstOrDefaultAsync(_context.ToDoItems, i => i.Id == setCompletedTrueCommand.ToDoItemId);
            item.IsCompleted = true;
            await _context.SaveChangesAsync();
            return item.Id;
        }
        
        public async Task<Result<ToDoItemDto, ErrorsEnum>> IfMissedToDoItem(IfMissedToDoItemQuery ifMissedToDoItemCommand)
        {
            var item = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions
                .FirstOrDefaultAsync(_context.ToDoItems, i => i.Id == ifMissedToDoItemCommand.Id);
            var dto = _dxo.Map(item);
            return dto;
        }
    }
}