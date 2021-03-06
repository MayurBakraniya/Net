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
    public class CoursesController : Controller
    {
        private readonly collegeContext _context;

        HttpClient client = new HttpClient();
        string url = "https://localhost:44329/api/Courses/";

        public CoursesController(collegeContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(string searchby,string search)
        {
            if(searchby == "Course Name")
            {
                return View(JsonConvert.DeserializeObject<List<Courses>>
                (await client.GetStringAsync(url+"searchbyname?name="+search)).ToList());
            }
            else if(searchby == "Duration")
            {
                return View(JsonConvert.DeserializeObject<List<Courses>>
                (await client.GetStringAsync(url + "searchbyduration?duration=" + search)).ToList());
            }
            return View(JsonConvert.DeserializeObject<List<Courses>>
                (await client.GetStringAsync(url)).ToList());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var courses = await _context.Courses
            //    .FirstOrDefaultAsync(m => m.CourseId == id);

            var courses = JsonConvert.DeserializeObject<Courses>
                (await client.GetStringAsync(url + id));
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,Duration")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(courses);
                //await _context.SaveChangesAsync();
                await client.PostAsJsonAsync<Courses>(url, courses);
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var courses = await _context.Courses.FindAsync(id);
            var courses = JsonConvert.DeserializeObject<Courses>
                (await client.GetStringAsync(url + id));
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,Duration")] Courses courses)
        {
            if (id != courses.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(courses);
                    //await _context.SaveChangesAsync();
                    await client.PutAsJsonAsync<Courses>(url + id.ToString(), courses);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesExists(courses.CourseId))
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
            return View(courses);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var courses = await _context.Courses
            //    .FirstOrDefaultAsync(m => m.CourseId == id);
            var courses = JsonConvert.DeserializeObject<Courses>
                (await client.GetStringAsync(url + id));
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var courses = await _context.Courses.FindAsync(id);
            //_context.Courses.Remove(courses);
            //await _context.SaveChangesAsync();
            await client.DeleteAsync(url + id);
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
