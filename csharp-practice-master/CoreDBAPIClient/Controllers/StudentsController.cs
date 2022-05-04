using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreDBAPIClient.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CoreDBAPIClient.Controllers
{
    public class StudentsController : Controller
    {
        private readonly collegeContext _context;

        HttpClient client = new HttpClient();
        string url = "https://localhost:44329/api/Students/";
        string courseurl = "https://localhost:44329/api/Courses/";
        

        public StudentsController(collegeContext context)
        {
            
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string searchby, string search)
        {
            //var collegeContext = _context.Students.Include(s => s.Course);

            if(searchby == "Name")
            {
                return View(JsonConvert.DeserializeObject<List<Students>>
                    (await client.GetStringAsync(url+"searchbyname?name="+search)).ToList());
            }
            else if(searchby == "Age")
            {
                return View(JsonConvert.DeserializeObject<List<Students>>
                    (await client.GetStringAsync(url + "searchbyage?age=" + search)).ToList());
            }
            else if(searchby == "Gender")
            {
                return View(JsonConvert.DeserializeObject<List<Students>>
                    (await client.GetStringAsync(url + "searchbygender?gender=" + search)).ToList());
            }
            else if(searchby == "Year")
            {
                return View(JsonConvert.DeserializeObject<List<Students>>
                    (await client.GetStringAsync(url + "searchbyyear?year=" + search)).ToList());
            }
            else if(searchby == "Course")
            {
                return View(JsonConvert.DeserializeObject<List<Students>>
                    (await client.GetStringAsync(url + "searchbycourse?course=" + search)).ToList());
            }
            return View(JsonConvert.DeserializeObject<List<Students>>(
                await client.GetStringAsync(url)).ToList());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var students = await _context.Students
            //    .Include(s => s.Course)
            //    .FirstOrDefaultAsync(m => m.StudentId == id);
            var students = JsonConvert.DeserializeObject<Students>(
                await client.GetStringAsync(url + id));
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentName,Age,Gender,CourseId,Doj")] Students students)
        {
            if (ModelState.IsValid)
            {
                await client.PostAsJsonAsync<Students>(url, students);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", students.CourseId);
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var students = await _context.Students.FindAsync(id);
            var students = JsonConvert.DeserializeObject<Students>(
                await client.GetStringAsync(url + id));
            if (students == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", students.CourseId);
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,Age,Gender,CourseId,Doj")] Students students)
        {
            if (id != students.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(students);
                    //await _context.SaveChangesAsync();
                    await client.PutAsJsonAsync<Students>(url + id.ToString(), students);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var courses = JsonConvert.DeserializeObject<IEnumerable<Students>>
                (await client.GetStringAsync(courseurl));
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", students.CourseId);
            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = JsonConvert.DeserializeObject<Students>(
                await client.GetStringAsync(url + id));
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var students = await _context.Students.FindAsync(id);
            //_context.Students.Remove(students);
            //await _context.SaveChangesAsync();
            await client.DeleteAsync(url + id);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
