using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.Commands;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using ToDo.DTO;


namespace Data.Abstraction
{
    public interface IAuthRepository
    {
        Task<User> Register( RegisterUserCommand request);
        Task<Result<TokenDto, ErrorsEnum>> Login(LoginUserCommand request);

    }
}