using FitnessManagerApi.Data;
using FitnessManagerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessManagerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly FitnessDbContext _context;

        public SubscriptionsController(FitnessDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return await _context.Subscriptions
                .Include(s => s.Member)
                .OrderBy(s => s.MemberId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Subscription>>> SearchSubscriptions([FromQuery] string description)
        {
            if (string.IsNullOrEmpty(description)) return BadRequest("Description is required.");
            return await _context.Subscriptions
                .Include(s => s.Member)
                .Where(s => s.Description.Contains(description))
                .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Subscription>> GetSubscription(int id)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.Member) 
                .FirstOrDefaultAsync(s => s.Id == id);
            if (subscription == null) return NotFound();
            return subscription;
        }

        [HttpPost]
        public async Task<ActionResult<Subscription>> PostSubscription(Subscription subscription)
        {
            var memberExists = await _context.Members.AnyAsync(m => m.Id == subscription.MemberId);
            if (!memberExists)
            {
                return BadRequest("Member with the specified MemberId does not exist.");
            }

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSubscription), new { id = subscription.Id }, subscription);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscription(int id, Subscription subscription)
        {
            if (id != subscription.Id) return BadRequest();
            _context.Entry(subscription).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null) return NotFound();
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}