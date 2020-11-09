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
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Result<User, Error>> Login(LoginUserCommand loginUserCommand)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginUserCommand.Username);
            if (user == null)
            {
                return new Error(ErrorsEnum.UserNotFound, $"User '{loginUserCommand.Username}' not found");
            }

            var userSighningResult = await _userManager.CheckPasswordAsync(user, loginUserCommand.Password);
            if (userSighningResult)
            {
                return new User();
            }
            return new Error(ErrorsEnum.BadRequest, "Something went wrong");
        }
        

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordSalt = hmac.Key;
            }
        }
    }
}