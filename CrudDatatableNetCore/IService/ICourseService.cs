using CrudDatatableNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDatatableNetCore.IService
{
    public interface ICourseService
    {
        List<Course> GetCourses();
        Course GetById(int CourseId);
        void Save(Course oCourse);
        void Update(Course oCourse);
        string Delete(int CourseId);
    }
}
