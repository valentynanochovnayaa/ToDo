using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Data.Abstraction;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.DTO;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IMapper mapper, IAuthRepository repo, IConfiguration config)
        {
            _mapper = mapper;
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            var userToCreate = _mapper.Map<User>(userForRegisterDto);
            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);
            return Ok();
        }
        
    }
}