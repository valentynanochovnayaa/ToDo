using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Common;
using Domain.DTO;
using Domain.Queries;
using MediatR;
using Moq;
using Services.Handlers.QueriesHandlers;
using Xunit;

namespace UnitTests
{
    public class GetToDoItemsQueryHandlerUnitTests
    {
        [Fact]
        public  async Task Handle_WithGetToDoItemsQuery_ShouldReturnToDoItems()
        {
            var repo = new Mock<IToDoItemRepository>();
            GetToDoItemsQuery query = new GetToDoItemsQuery();
            var toDoItemDtos = new List<ToDoItemDto>()
            {
                 new ToDoItemDto
                 {
                     Name = "Finish 5 books until this month"
                 }
                // new Fixture().Create<ToDoItemDto>()5
            };
            repo.Setup(r => 
                r.GetToDoItems(query)).ReturnsAsync(Result.Success<List<ToDoItemDto>, ErrorsEnum>(toDoItemDtos));

            GetToDoItemsQueryHandler handler = new GetToDoItemsQueryHandler(repo.Object);
            var result = await handler.Handle(query, new System.Threading.CancellationToken());
            
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Collection(result.Value, item => Assert.Contains("Finish 5 books until this month", item.Name));
            
        }
    }
}