using AutoMapper;
using Member.Application.DTOs;
using Member.Domain.Entities;

namespace Member.Application.Mapping
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            // Map from Entity -> DTO
            CreateMap<Members, MemberDto>();

            // Map from DTO -> Entity
            CreateMap<MemberDto, Members>();
        }
    }
}
