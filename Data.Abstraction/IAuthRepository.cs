using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.Commands;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ToDo.DTO;


namespace Data.Abstraction
{
    public interface IAuthRepository
    {
        Task<Result<Unit, ErrorsEnum>> Register( RegisterUserCommand request);
        Task<Result<TokenDto, ErrorsEnum>> Login(LoginUserCommand request);

    }
}