using NewsApi.Models;

namespace NewsApi.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}