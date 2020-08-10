using CrudDatatableNetCore.Context;
using CrudDatatableNetCore.IService;
using CrudDatatableNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDatatableNetCore.Service
{
    public class CourseService : ICourseService
    {
        private readonly DatabaseContext _context;

        public CourseService(DatabaseContext context)
        {
            _context = context;
        }


        public string Delete(int CourseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == CourseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }

            return "DELETE";
        }

        public Course GetById(int CourseId)
        {
            return _context.Courses.SingleOrDefault(c => c.CourseID == CourseId);
        }

        public List<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }

        public void Save(Course oCourse)
        {
            _context.Courses.Add(oCourse);
            _context.SaveChanges();
        }

        public void Update(Course oCourse)
        {
            _context.Courses.Update(oCourse);
            _context.SaveChanges();
        }
    }
}
