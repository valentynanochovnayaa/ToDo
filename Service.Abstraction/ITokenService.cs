using System.Web.Providers.Entities;
using Domain.Commands;
using User = Domain.Entities.User;

namespace ToDo.Data
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}