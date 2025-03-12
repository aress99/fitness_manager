using FitnessManagerApi.Data;
using FitnessManagerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly FitnessDbContext _context;

        public MembersController(FitnessDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }

        [HttpGet("{fitnessNumber}")]
        public async Task<ActionResult<Member>> GetMember(string fitnessNumber)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.FitnessNumber == fitnessNumber);
            if (member == null) return NotFound();
            return member;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            var existingMember = await _context.Members.FirstOrDefaultAsync(m => m.FitnessNumber == member.FitnessNumber);
            if (existingMember != null)
            {
                return BadRequest("A member with this FitnessNumber already exists.");
            }

            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMember), new { fitnessNumber = member.FitnessNumber }, member);
        }

        [HttpPut("{fitnessNumber}")]
        public async Task<IActionResult> PutMember(string fitnessNumber, Member member)
        {
            if (fitnessNumber != member.FitnessNumber) return BadRequest();
            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{fitnessNumber}")]
        public async Task<IActionResult> DeleteMember(string fitnessNumber)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.FitnessNumber == fitnessNumber);
            if (member == null) return NotFound();
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}