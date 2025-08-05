using ShopChain.Core.Entities;

namespace ShopChain.Core.Interfaces
{
    public interface IUserClientRepository
    {
        Task<UserClient> RegisterAsync(UserClient userClient, CancellationToken cancellationToken = default);
        Task<UserClient?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<UserClient?> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default);
    }
}
