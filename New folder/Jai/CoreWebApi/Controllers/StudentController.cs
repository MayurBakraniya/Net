using CoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>();


        public StudentController()
        {
            students.Add(new Student { StudId = 1, StudName = "Jai", course = "C++" });
            students.Add(new Student { StudId = 2, StudName = "Janvi", course = "Java" });
        }

        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return students.ToList();
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return students.SingleOrDefault(c => c.StudId == id);
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] Student newStudent)
        {
            Student studentToAdd = new Student();
            studentToAdd.StudId = newStudent.StudId;
            studentToAdd.StudName = newStudent.StudName;
            studentToAdd.course = newStudent.course;
            students.Add(studentToAdd);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Student studentUpdate)
        {
            Student studentToUpdate = Get(studentUpdate.StudId);
            studentToUpdate.StudName = studentUpdate.StudName;
            studentToUpdate.course = studentUpdate.course;

        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
