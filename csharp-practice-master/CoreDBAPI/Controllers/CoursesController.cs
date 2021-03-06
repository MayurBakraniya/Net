using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDBAPI.Models;

namespace CoreDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly collegeContext _context;

        public CoursesController(collegeContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses>> GetCourses(int id)
        {

            var courses = await _context.Courses.
                FirstOrDefaultAsync(c => c.CourseId == id);

            

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourses(int id, Courses courses)
        {
            if (id != courses.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(courses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Courses>> PostCourses(Courses courses)
        {
            _context.Courses.Add(courses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourses", new { id = courses.CourseId }, courses);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Courses>> DeleteCourses(int id)
        {
            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(courses);
            await _context.SaveChangesAsync();

            return courses;
        }

        [HttpGet("searchbyname")]
        public async Task<ActionResult<IEnumerable<Courses>>> searchbyname(string name)
        {
            var courses = await _context.Courses
                .Where(c => c.CourseName.Contains(name))
                .ToListAsync();

            if(courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        [HttpGet("searchbyduration")]
        public async Task<ActionResult<IEnumerable<Courses>>> searchbyduration(int duration)
        {
            var courses = await _context.Courses
                .Where(c => c.Duration.Equals(duration))
                .ToListAsync();

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        private bool CoursesExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
