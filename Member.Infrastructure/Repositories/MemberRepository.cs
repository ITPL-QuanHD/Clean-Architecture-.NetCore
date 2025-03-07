using Member.Application.IRepository;
using Member.Domain.Entities;
using Member.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Member.Infrastructure.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Members>> GetAllMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Members> GetMemberByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<bool> AddMemberAsync(Members member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> UpdateMemberAsync(Members member)
        {
            var memberToUpdate = _context.Members.Attach(member);
            memberToUpdate.State = EntityState.Modified;
            _context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public Task<bool> DeleteMemberAsync(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return Task.FromResult(false);
            _context.Members.Remove(member);
            _context.SaveChangesAsync();
            return Task.FromResult(true);
        }
    }
}
