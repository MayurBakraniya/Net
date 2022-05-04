using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class studentController : Controller
    {
        public static List<student> students = new List<student> {
            new student {Id=1,Name="Vivek",Age=20,city="Surat",totalmarks=455 } 
        };
        // GET: student
        public ActionResult Index(string searchby,string search)
        {
            //students.Add(new student { Id=1,Name="Vivek",Age=20,city="Surat",totalmarks=455});
            if(searchby == "Name")
            {
                var result = students.Where(x => x.Name == search || search == null).ToList();
                return View(result);
            }else if(searchby == "Age")
            {
                int value;
                if(search == null)
                {
                    value = 0;
                }
                else
                {
                    value = Convert.ToInt32(search);
                }
                
                var result = students.Where(x => x.Age == value || value == 0).ToList();
                return View(result);
            }
            else if (searchby == "City")
            {
                var result = students.Where(x => x.city == search || search == null).ToList();
                return View(result);
            }
            return View(students);
        }

        // GET: student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: student/Create
        [HttpPost]
        public ActionResult Create(student newstudent)
        {
            try
            {
                // TODO: Add insert logic here
                newstudent.Id = students.Count + 1;
                students.Add(newstudent);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: student/Edit/5
        public ActionResult Edit(int id)
        {
            
            student editstudent = students.Single(s => s.Id==id);
            return View(editstudent);
        }

        // POST: student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                student editstudent = students.Single(s => s.Id==id);
                if (TryUpdateModel(editstudent))
                {
                    return RedirectToAction("Index");
                }
                return View(editstudent);
            }
            catch
            {
                return View();
            }
        }

        // GET: student/Delete/5
        public ActionResult Delete(int id)
        {
            student deletestudent = students.Single(s => s.Id==id);
            return View(deletestudent);
        }

        // POST: student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                student deletestudent = students.Single(s => s.Id == id);
                students.Remove(deletestudent);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
