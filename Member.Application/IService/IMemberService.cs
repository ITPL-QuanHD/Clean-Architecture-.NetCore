using Member.Application.DTOs;

namespace Member.Application.IService
{
    public interface IMemberService
    {
        Task<List<MemberDto>> GetAllMembersAsync();
        Task<MemberDto> GetMemberByIdAsync(int id);
        Task<bool> AddMemberAsync(MemberDto memberDto);
        Task<bool> UpdateMemberAsync(MemberDto memberDto);
        Task<bool> DeleteMemberAsync(int id);
    }
}
