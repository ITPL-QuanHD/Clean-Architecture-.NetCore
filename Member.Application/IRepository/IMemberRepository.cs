using Member.Domain.Entities;

namespace Member.Application.IRepository
{
    public interface IMemberRepository
    {
        Task<List<Members>> GetAllMembersAsync();
        Task<Members> GetMemberByIdAsync(int id);
        Task<bool> AddMemberAsync(Members member);
        Task<bool> UpdateMemberAsync(Members member);
        Task<bool> DeleteMemberAsync(int id);
    }
}
