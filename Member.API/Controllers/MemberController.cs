using Microsoft.AspNetCore.Mvc;
using Member.Application.DTOs;
using Member.Application.IService;


namespace Member.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<MemberController> _logger;

        public MemberController(IMemberService memberService, ILogger<MemberController> logger)
        {
            _memberService = memberService;
            _logger = logger;
        }

        /// <summary>
        /// Get all members.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<MemberDto>>> GetAllMembers()
        {
            try
            {
                var members = await _memberService.GetAllMembersAsync();
                return Ok(members);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting member list.");
                return StatusCode(500, "An error occurred while retrieving the member list.");
            }
        }

        /// <summary>
        /// Get member by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetMemberById(int id)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                    return NotFound($"No member found with ID = {id}");

                return Ok(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting member with ID = {id}");
                return StatusCode(500, $"An error occurred while retrieving member with ID = {id}");
            }
        }

        /// <summary>
        /// Add new member.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddMember([FromBody] MemberDto memberDto)
        {
            try
            {
                if (memberDto == null)
                    return BadRequest("Invalid data.");

                await _memberService.AddMemberAsync(memberDto);
                return CreatedAtAction(nameof(GetMemberById), new { id = memberDto.Id }, memberDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding member.");
                return StatusCode(500, "An error occurred while adding members.");
            }
        }

        /// <summary>
        /// Update member by ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMember(int id, [FromBody] MemberDto memberDto)
        {
            try
            {
                if (memberDto == null || memberDto.Id != id)
                    return BadRequest("Invalid ID.");

                await _memberService.UpdateMemberAsync(memberDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating member with ID = {id}");
                return StatusCode(500, $"An error occurred while updating member with ID = {id}");
            }
        }

        /// <summary>
        /// Delete member by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMember(int id)
        {
            try
            {
                await _memberService.DeleteMemberAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting member with ID = {id}");
                return StatusCode(500, $"An error occurred while deleting member with ID = {id}");
            }
        }
    }
}
