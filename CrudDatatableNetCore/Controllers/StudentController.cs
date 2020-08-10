using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudDatatableNetCore.IService;
using CrudDatatableNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDatatableNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentService _studentService = null;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // GET: api/Student
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _studentService.GetStudents();
        }

        // GET: api/Student/5
        //Name = "Get"
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _studentService.GetById(id);
        }

        // POST: api/Student
        [HttpPost]
        public void SaveOrUpdate([FromForm] Student student)
        {
            if (student.StudentID == 0) _studentService.Save(student);
            else _studentService.Update(student);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _studentService.Delete(id);
        }
    }
}
