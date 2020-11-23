using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Data.Data;
using Domain.Commands;
using Domain.Common;
using Domain.DTO;
using Domain.Entities;
using Domain.Queries;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Services.Dxos;
using ToDo;
using ToDo.DTO;
using Xunit;

namespace IntegrationTests
{
    public class SetCompletedItemCommandHandlerIntegrationTest
    {
        private readonly IToDoItemRepository _repo;
        private readonly IAuthRepository _userrepo;
        private readonly DataContext _context;
        private readonly HttpClient _client;
        private readonly TestServer _server;

        public SetCompletedItemCommandHandlerIntegrationTest()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                    webHost.UseEnvironment("Testing");
                });

            // Build and start the IHost
            var host = hostBuilder.StartAsync().Result;
            _server = host.GetTestServer();
            _context = host.Services.GetService(typeof(DataContext)) as DataContext;
            _client = host.GetTestClient();
            //_repo = new ToDoItemRepository(_context, new ToDoItemDxo());
            _repo = host.Services.GetService<IToDoItemRepository>();
            _userrepo = host.Services.GetService<IAuthRepository>();
            
        }

        public async Task<Result<Guid, ErrorsEnum>> SetCompletedTrue(SetCompletedTrueCommand command)
        {
            var item = await _context.ToDoItems.FirstOrDefaultAsync(i => i.Id == command.Id);
            item.IsCompleted = true;
            await _context.SaveChangesAsync();
            return item.Id;

        }

        public async Task<ToDoItemDto> GetToDoItem(GetToDoItemQuery query)
        {
            var item = await _context.ToDoItems.FirstOrDefaultAsync(i => i.Id == query.Id);
            var dto = new ToDoItemDto
            {
                Name = item.Name,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                Deadline = item.Deadline
            };
            return dto;
        }

        [Fact]
        public async Task SetCompletedItem_ValidParams_ShouldReturnId()
        {
            //arrange
            var command = new CreateToDoItemCommand();
            var item = await _repo.CreateToDoItem(command);
            var id = item.Value;
            var setCompletedCommand = new SetCompletedTrueCommand(false);

            //act
            //var sut = server.Host.Services.GetService<IC>();
            var setCompleted = await SetCompletedTrue(setCompletedCommand);
            //assert
            Assert.True(setCompleted.IsSuccess);
            var query = new GetToDoItemQuery();
            query.Id = setCompleted.Value;
            var returnItem = await GetToDoItem(query);
            Assert.True(returnItem.IsCompleted);
        }

        [Fact]
        public async Task SetCompletedItem_WithValidParams_ShouldReturn200()
        {
            #region Arrange
            var userManager = _server.Services.GetService<UserManager<User>>();
            var user = new User()
            {
                Email = "test@test.test",
                UserName = "test"
            };
            var identityResult = await userManager.CreateAsync(user, "qwerty123");

            var toDoItem = new ToDoItem()
            {
                Deadline = DateTimeOffset.Now.AddDays(2),
                Description = "",
                Name = "Eat sandwich",
                UserId = user.Id
            };
            await _context.ToDoItems.AddAsync(toDoItem);
            await _context.SaveChangesAsync();
            #endregion

            #region Act
            //_client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var sut = await _client
                .PutAsync($"users/{user.Id.ToString()}/todoitems/{toDoItem.Id.ToString()}/completed", toDoItem);
            await _context.SaveChangesAsync();
            #endregion

            #region Assert
            sut.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            var response = await sut.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Guid>(response);
            var getItem = await _client
                .GetAsync($"users/{user.Id.ToString()}/todoitems/{toDoItem.Id.ToString()}/get");
                //await _context.ToDoItems.FirstOrDefaultAsync(i => i.Id == content);
                var resp = await getItem.Content.ReadAsStringAsync();
                var cont = JsonConvert.DeserializeObject<ToDoItem>(resp);
            //getItem.IsCompleted.Should().Be(true);
            cont.IsCompleted.Should().Be(true);

            #endregion
        }

        [Fact]
        public async Task SetCompletedItemAuthorize_WithValidParams_ShouldReturn200()
        {
            #region Arrange
            var userManager = _server.Services.GetService<UserManager<User>>();
            var user = new User()
            {
                Email = "test@test.test",
                UserName = "test"
            };
            var identityResult = await userManager.CreateAsync(user, "qwerty123");
            var loginUser = new LoginUserCommand
            {
                Username = "test",
                Password = "qwerty123"
            };
            var item = new CreateToDoItemCommand("test", "test", DateTimeOffset.Now, false);
            var sut = await _client.PostAsync($"login", loginUser);
            var response = await sut.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<TokenDto>(response);
            await _context.SaveChangesAsync();
            var token = dto.Token;
                
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", $"Bearer {token}");
            var newSut = await _client.PostAsync($"users/{user.Id.ToString()}/todoitems", item);
            var resp = await newSut.Content.ReadAsStringAsync();
            await _context.SaveChangesAsync();
            
            var updateItem = new UpdateToDoItemCommand
            {
                Name = "testtest",
                Deadline = DateTimeOffset.Now
            };
            var updateId = JsonConvert.DeserializeObject(resp);
            var updateSut = await _client.PutAsync($"todoitems/{updateId}", updateItem);
            var updateResponse = await updateSut.Content.ReadAsStringAsync();
            await _context.SaveChangesAsync();
            
            var ifMissed = new IfMissedToDoItemQuery();
            var ifMissedId = JsonConvert.DeserializeObject(updateResponse);
            var ifMissedSut = await _client
                .GetAsync($"users/{user.Id.ToString()}/todoitems/{ifMissedId}/ifmissed");
            var ifMissedResponse = await ifMissedSut.Content.ReadAsStringAsync();
            
            var setCompleted = new SetCompletedTrueCommand(true);
            var setItemId = JsonConvert.DeserializeObject(updateResponse);
            var setAsTrueSut =
                await _client.
                    PutAsync($"users/{user.Id.ToString()}/todoitems/{setItemId}/completed",
                    setCompleted);
            await _context.SaveChangesAsync();
            
            var getItemSut = await _client.GetAsync($"users/{user.Id.ToString()}/todoitems/{setItemId}/get");
            var getResponse = await getItemSut.Content.ReadAsStringAsync();
            
            var deleteItem = new DeleteToDoItemCommand();
            var deleteId = JsonConvert.DeserializeObject(updateResponse);
            var deleteSut = await _client.DeleteAsync($"todoitems/{deleteId}", deleteItem);
            await _context.SaveChangesAsync();


            #endregion

            #region Act



            #endregion

            #region Assert



            #endregion
        }
    }

    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient httpClient, string requestUri, T data)
        {
            var jsonPostRequest = JsonConvert.SerializeObject(data);
            var postRequest = new StringContent(jsonPostRequest, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(requestUri, postRequest);
        }

        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient httpClient, string requestUri, T data)
        {
            var jsonPostRequest = JsonConvert.SerializeObject(data);
            var postRequest = new StringContent(jsonPostRequest, Encoding.UTF8, "application/json");
            return httpClient.PutAsync(requestUri, postRequest);
        }
        
        public static Task<HttpResponseMessage> DeleteAsync<T>(this HttpClient httpClient, string requestUri, T data)
        {
            var jsonPostRequest = JsonConvert.SerializeObject(data);
            var postRequest = new StringContent(jsonPostRequest, Encoding.UTF8, "application/json");
            return httpClient.PutAsync(requestUri, postRequest);
        }
    }
    public static class HttpResponseMessageExtensionMethods
    {
        public static async Task<T> GetContentAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<T>(response);
            return content;
        }
    }
}