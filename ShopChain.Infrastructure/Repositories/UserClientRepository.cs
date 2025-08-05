using Microsoft.EntityFrameworkCore;
using ShopChain.Application.Commons;
using ShopChain.Core.Entities;
using ShopChain.Core.Interfaces;
using ShopChain.Infrastructure.Data;
using ShopChain.Infrastructure.Security;

namespace ShopChain.Infrastructure.Repositories
{
    public class UserClientRepository : IUserClientRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserClientRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _passwordHasher = new Sha256PasswordHasher(); 
        }

        public async Task<UserClient?> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            var userClient = await _context.UserClients
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
            if (userClient == null)
            {
                return null;                
            }   

            if (!_passwordHasher.VerifyPassword(password, userClient.PasswordHash))
            {
                return null;
            }

            return userClient;
        }

        public async Task<UserClient?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _context.UserClients
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
        }

        public async Task<UserClient> RegisterAsync(UserClient userClient, CancellationToken cancellationToken = default)
        {
            _context.UserClients.Add(userClient);
            return await _context.SaveChangesAsync(cancellationToken) > 0 ? userClient : null!;
        }
    }
}
