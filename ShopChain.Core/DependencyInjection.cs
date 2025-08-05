using Microsoft.Extensions.DependencyInjection;

namespace ShopChain.Core
{
    /// <summary>
    /// Đăng ký các dịch vụ thuộc layer Core
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Đăng ký các dependency trong Core Layer
        /// </summary>
        public static IServiceCollection AddCoreDI(this IServiceCollection services)
        {
            // Hiện tại không có service nội bộ cần đăng ký trong Core
            // Có thể thêm Validator, ServiceHelper, RuleEngine,... nếu cần sau này

            return services;
        }
    }
}
