using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data.Abstraction;
using Domain.Commands;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Abstraction.Dxos;
using ToDo.Data;
using ToDo.DTO;

namespace Data.Data
{
    public class AuthRepository: IAuthRepository
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IToDoUserDxo _dxo;
        private readonly UserManager<User> _userManager;
        public AuthRepository(DataContext context, ITokenService tokenService, IToDoUserDxo dxo, UserManager<User> userManager)
        {
            _context = context;
            _tokenService = tokenService;
            _dxo = dxo;
            _userManager = userManager;
        }
        public async Task<User> Register(RegisterUserCommand registerUserCommand)
        {
            var user = _dxo.Map(registerUserCommand);
            var result = await _userManager.CreateAsync(user, registerUserCommand.Password);
            return user;
        }

        public async Task<Result<TokenDto, ErrorsEnum>> Login(LoginUserCommand loginUserCommand)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginUserCommand.Username);
            if (user == null)
            {
                return ErrorsEnum.UserNotFound;
            }
            var userSighningResult = await _userManager.CheckPasswordAsync(user, loginUserCommand.Password);
            if (userSighningResult)
            {
                var token = _tokenService.CreateToken(user);
                return new TokenDto(token);
            }
            return ErrorsEnum.BadRequest;
        }
    }
}