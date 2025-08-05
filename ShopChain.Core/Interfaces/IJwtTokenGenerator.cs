using ShopChain.Core.Entities;

namespace ShopChain.Core.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserClient user);
    }
}
