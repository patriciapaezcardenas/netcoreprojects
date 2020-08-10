using CrudDatatableNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDatatableNetCore.IService
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        Student GetById(int StudentId);
        void Save(Student oStudent);
        void Update(Student oStudent);
        string Delete(int studentId);

    }
}
