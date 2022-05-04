using DemoCrudCS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoCrudCS.Controllers
{
    public class CourseController : Controller
    {
        private readonly demoContext _context;

        public CourseController(demoContext context)
        {
            _context = context;
        }

        // GET: CourseController
        public async Task<ActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        // GET: CourseController/Details/5
        public async Task<ActionResult> Details(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.Id == courseId);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // GET: CourseController/Create
        public async Task<ActionResult> CreateAsync(int? courseId)
        {
            ViewBag.PageName = courseId == null ? "Create Course" : "Edit Course";
            ViewBag.IsEdit = courseId == null ? false : true;
            if (courseId == null)
            {
                return View();
            }
            else
            {
                var course = await _context.Courses.FindAsync(courseId);

                if (course == null)
                {
                    return NotFound();
                }
                return View(course);
            }
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int courseId, [Bind("Id,Name")] Course CourseData)
        {
            bool IsCourseExist = false;

            Course course = await _context.Courses.FindAsync(courseId);

            if (course != null)
            {
                IsCourseExist = true;
            }
            else
            {
                course = new Course();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    course.Name = CourseData.Name;

                    if (IsCourseExist)
                    {
                        _context.Update(course);
                    }
                    else
                    {
                        _context.Add(course);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(CourseData);
        }

        //// GET: CourseController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: CourseController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: CourseController/Delete/5
        public async Task<ActionResult> Delete(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.Id == courseId);

            return View(course);

        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
