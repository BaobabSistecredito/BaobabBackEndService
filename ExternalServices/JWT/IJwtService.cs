using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.ExternalServices.Jwt
{
    public interface IJwtService
    {
        string GenerateJwtToken(MarketingUser user);
    }
}