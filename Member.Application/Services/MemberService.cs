using AutoMapper;
using Member.Application.DTOs;
using Member.Application.IRepository;
using Member.Application.IService;
using Member.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Member.Application.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MemberService> _logger;

        public MemberService(IMemberRepository memberRepository, IMapper mapper, ILogger<MemberService> logger)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<MemberDto>> GetAllMembersAsync()
        {
            try
            {
                var members = await _memberRepository.GetAllMembersAsync();
                return _mapper.Map<List<MemberDto>>(members);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                throw new Exception("An error occurred while retrieving the member list.", ex);
            }
        }

        public async Task<MemberDto> GetMemberByIdAsync(int id)
        {
            try
            {
                var member = await _memberRepository.GetMemberByIdAsync(id);
                if (member == null)
                {
                    _logger.LogWarning($"No member found with ID = {id}");
                    return null;
                }
                return _mapper.Map<MemberDto>(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting member with ID = {id}");
                throw new Exception($"An error occurred while retrieving member with ID. = {id}", ex);
            }
        }

        public async Task<bool> AddMemberAsync(MemberDto memberDto)
        {
            try
            {
                var member = _mapper.Map<Members>(memberDto);
                await _memberRepository.AddMemberAsync(member);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding member.");
                throw new Exception("An error occurred while adding members.", ex);
            }
        }

        public async Task<bool> UpdateMemberAsync(MemberDto memberDto)
        {
            try
            {
                var member = _mapper.Map<Members>(memberDto);
                await _memberRepository.UpdateMemberAsync(member);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating member with ID = {memberDto.Id}");
                throw new Exception($"An error occurred while updating member with ID = {memberDto.Id}", ex);
            }
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            try
            {
                await _memberRepository.DeleteMemberAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting member with ID = {id}");
                throw new Exception($"An error occurred while deleting member with ID = {id}", ex);
            }
        }
    }
}
