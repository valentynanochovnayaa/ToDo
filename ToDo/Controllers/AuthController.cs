using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Data.Abstraction;
using Domain.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.DTO;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ApiControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand request)
        {
            return await SendRequestAsync(request);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand request)
        {
            return await SendRequestAsync(request);
        }
        
        
    }
}