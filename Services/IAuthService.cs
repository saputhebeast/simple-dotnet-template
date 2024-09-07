using UrbanNest.Models;

namespace UrbanNest.Services;

public interface IAuthService
{
    public string GenerateJwtToken(User user);
}
