using SmartInventoryBE.Models;

namespace SmartInventoryBE.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
        bool ValidateToken(string token);
    }
}
