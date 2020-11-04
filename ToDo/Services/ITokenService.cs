using ToDo.Models;

namespace ToDo.Data
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}