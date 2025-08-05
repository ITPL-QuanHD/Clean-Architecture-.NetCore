using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShopChain.Application;
using ShopChain.Core;
using ShopChain.Infrastructure;
using System.Security.Claims;
using System.Text;

namespace ShopChain.Api
{
    /// <summary>
    /// Đăng ký toàn bộ DI của ứng dụng từ các tầng
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("Jwt");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        RoleClaimType = ClaimTypes.Role,

                        ValidIssuer = jwtConfig["Issuer"],
                        ValidAudience = jwtConfig["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtConfig["Key"]!))
                    };
                });
            services.AddAuthorization();
            return services
                .AddApplicationDI()
                .AddInfrastructureDI(configuration)
                .AddCoreDI();
        }
    }
}
