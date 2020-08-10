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
    public class CourseController : ControllerBase
    {
        ICourseService _courseService = null;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }





        // GET: api/Course
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return _courseService.GetCourses();
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public Course Get(int id)
        {
            return _courseService.GetById(id);
        }

        // POST: api/Course
        [HttpPost]
        public void SaveOrUpdate([FromForm] Course course)
        {
            if (course.CourseID == 0) _courseService.Save(course);
            else _courseService.Update(course);
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _courseService.Delete(id);
        }
    }
}
