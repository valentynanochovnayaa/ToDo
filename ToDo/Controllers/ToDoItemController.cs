using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Data;
using Domain.Commands;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ToDo.Controllers
{
    //[Authorize]
    public class ToDoItemController : ApiControllerBase
    {
        private readonly DataContext _context;
        public ToDoItemController(IMediator mediator, DataContext context) : base(mediator)
        {
            _context = context;
        }
        
        [Authorize]
        [HttpPost("users/{userId}/todoitems")]
        public async Task<IActionResult> CreateToDoItem([FromBody]CreateToDoItemCommand request)
        {
            var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.UserId = Guid.Parse(userid);
            return await SendRequestAsync(request);
        }
        [Authorize]
        [HttpPut("users/{userId}/todoitems/{id}")]
        public async Task<IActionResult> UpdateToDoItem([FromBody]UpdateToDoItemCommand request,[FromRoute]Guid id)
         {
             var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
             request.Id = id;
             request.UserId = Guid.Parse(userid);
             return await SendRequestAsync(request);
        }
        [Authorize]
        [HttpDelete("users/{userId}/todoitems/{id}")]
        public async Task<IActionResult> DeleteToDoItem(DeleteToDoItemCommand request, [FromRoute]Guid id)
        {
            var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.Id = id;
            request.UserId = Guid.Parse(userid);
            return await SendRequestAsync(request);
        }
        [HttpGet("users/{userId}/todoitems/get")]
        public async Task<IActionResult> GetToDoItems(GetToDoItemsQuery request)
        {
            var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.UserId = Guid.Parse(userid);
            return await SendRequestAsync(request);
        }

        [HttpGet("users/{userId}/todoitems/{id}/get")]
        public async Task<IActionResult> GetToDoItem([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            return await SendRequestAsync(new GetToDoItemQuery(id, userId));
        }
        
        [Authorize]
        [HttpPut("users/{userId}/todoitems/{id}/completed")]
        public async Task<IActionResult> SetCompletedTrue([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            try
            {
                return await SendRequestAsync(new SetCompletedTrueCommand(id, userId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpGet("users/{userId}/todoitems/{id}/ifmissed")]
        public async Task<IActionResult> IfMissedToDoItem([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            return await SendRequestAsync(new IfMissedToDoItemQuery(id, userId));
        }
    }
}