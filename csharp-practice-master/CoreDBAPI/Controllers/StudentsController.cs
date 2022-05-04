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
    public class StudentsController : ControllerBase
    {
        private readonly collegeContext _context;

        public StudentsController(collegeContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudents()
        {
            return await _context.Students.Include(s => s.Course).ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudents(int id)
        {
            var students = await _context.Students.
                Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudents(int id, Students students)
        {
            if (id != students.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(students).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Students>> PostStudents(Students students)
        {
            _context.Students.Add(students);

            await _context.SaveChangesAsync();



            return CreatedAtAction("GetStudents", new { id = students.StudentId }, students);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Students>> DeleteStudents(int id)
        {
            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            _context.Students.Remove(students);
            await _context.SaveChangesAsync();

            return students;
        }

        [HttpGet("searchbyname")]
        public async Task<ActionResult<IEnumerable<Students>>> searchbyname(string name)
        {
            var students = await _context.Students.
                Include(s => s.Course)
                .Where(s => s.StudentName.Contains(name))
                .ToListAsync();

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        [HttpGet("searchbygender")]
        public async Task<ActionResult<IEnumerable<Students>>> searchbygender(string gender)
        {
            var students = await _context.Students.
                Include(s => s.Course)
                .Where(s => s.Gender == gender)
                .ToListAsync();

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        [HttpGet("searchbycourse")]
        public async Task<ActionResult<IEnumerable<Students>>> searchbycourse(string course)
        {
            var students = await _context.Students.
                Include(s => s.Course)
                .Where(s => s.Course.CourseName.Contains(course))
                .ToListAsync();

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        [HttpGet("searchbyyear")]
        public async Task<ActionResult<IEnumerable<Students>>> searchbyyear(int year)
        {
            var students = await _context.Students.
                Include(s => s.Course)
                .Where(s => s.Doj.Year.Equals(year))
                .ToListAsync();

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        [HttpGet("searchbyage")]
        public async Task<ActionResult<IEnumerable<Students>>> searchbyage(int age)
        {
            var students = await _context.Students.
                Include(s => s.Course)
                .Where(s => s.Age.Equals(age))
                .ToListAsync();

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
