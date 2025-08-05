using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopChain.Application.Commons;
using ShopChain.Core.Interfaces;
using ShopChain.Infrastructure.Data;
using ShopChain.Infrastructure.Repositories;
using ShopChain.Infrastructure.Security;


namespace ShopChain.Infrastructure
{
    /// <summary>
    /// Đăng ký các dependency cho tầng Infrastructure
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký DbContext với chuỗi kết nối từ cấu hình
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Đăng ký Repository
            services.AddScoped<IPasswordHasher, Sha256PasswordHasher>();

            services.AddScoped<IUserClientRepository, UserClientRepository>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}
