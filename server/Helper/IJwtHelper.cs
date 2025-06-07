using server.Entities;
using System.Security.Claims;

namespace server.Helper
{
    public interface IJwtHelper
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
