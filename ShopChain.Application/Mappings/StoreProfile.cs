using AutoMapper;
using ShopChain.Application.Dtos;
using ShopChain.Core.Entities;

namespace ShopChain.Application.Mappings
{
    /// <summary>
    /// Cấu hình ánh xạ giữa Entity và DTO
    /// </summary>
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<UserClient, UserClientDto>().ReverseMap();
        }
    }
}
