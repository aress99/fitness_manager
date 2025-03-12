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
    public class WorkoutsController : ControllerBase
    {
        private readonly FitnessDbContext _context;

        public WorkoutsController(FitnessDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return await _context.Workouts
                .Include(w => w.Member)
                .OrderBy(w => w.MemberId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Workout>>> SearchWorkouts([FromQuery] DateTime workoutDate)
        {
            return await _context.Workouts
                .Include(w => w.Member)
                .Where(w => w.WorkoutDate.Date == workoutDate.Date)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkout(int id)
        {
            var workout = await _context.Workouts
                .Include(w => w.Member) // Включва свързания Member
                .FirstOrDefaultAsync(w => w.Id == id);
            if (workout == null) return NotFound();
            return workout;
        }

        [HttpPost]
        public async Task<ActionResult<Workout>> PostWorkout(Workout workout)
        {
            var memberExists = await _context.Members.AnyAsync(m => m.Id == workout.MemberId);
            if (!memberExists)
            {
                return BadRequest("Member with the specified MemberId does not exist.");
            }

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetWorkout), new { id = workout.Id }, workout);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout(int id, Workout workout)
        {
            if (id != workout.Id) return BadRequest();
            _context.Entry(workout).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null) return NotFound();
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}