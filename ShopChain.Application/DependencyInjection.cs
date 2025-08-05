using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShopChain.Application.Commands.UserClientCommands;
using System.Reflection;
using FluentValidation;


namespace ShopChain.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            // Đăng ký AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Đăng ký FluentValidation 
            services.AddValidatorsFromAssemblyContaining<RegisterUserClientCommandValidator>();
            return services;
        }
    }
}
