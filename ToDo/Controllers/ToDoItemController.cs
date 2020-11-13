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
using Microsoft.IdentityModel.JsonWebTokens;

namespace ToDo.Controllers
{
    [Authorize]
    public class ToDoItemController : ApiControllerBase
    {
        private readonly DataContext _context;
        public ToDoItemController(IMediator mediator, DataContext context) : base(mediator)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateToDoItem([FromBody]CreateToDoItemCommand request)
        {
            var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.UserId = Guid.Parse(userid); 
            // var token = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);
            //if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            //{
                //return BadRequest("Name identifier from claims cannot be parsed to GUID.");
            //}
            return await SendRequestAsync(request);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateToDoItem([FromBody]UpdateToDoItemCommand request, Guid id)
         {
            var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = await _context.ToDoItems.FirstOrDefaultAsync(i => i.Id == id);
            request.Id = item.Id;
            request.UserId = Guid.Parse(userid);
            return await SendRequestAsync(request);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteToDoItem(DeleteToDoItemCommand request, Guid id)
        {
            var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = await _context.ToDoItems.FirstOrDefaultAsync(i => i.Id == id);
            request.Id = item.Id;
            request.UserId = Guid.Parse(userid);
            return await SendRequestAsync(request);
        }

        [HttpGet]
        public async Task<IActionResult> GetToDoItems(GetToDoItemsQuery request)
        {
            var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.UserId = Guid.Parse(userid);
            return await SendRequestAsync(request);
        }

    }
}