using System.Web.Providers.Entities;

namespace ToDo.Data
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}