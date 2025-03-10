using Member.Application.DTOs;


namespace Member.Application.IRepository
{
    public interface IMemberRepository
    {
        Task<List<MemberDto>> GetAllMembersAsync();
        Task<MemberDto> GetMemberByIdAsync(int id);
        Task<bool> AddMemberAsync(MemberDto member);
        Task<bool> UpdateMemberAsync(MemberDto member);
        Task<bool> DeleteMemberAsync(int id);
    }
}
