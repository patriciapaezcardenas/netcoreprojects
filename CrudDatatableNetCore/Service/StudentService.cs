using CrudDatatableNetCore.Context;
using CrudDatatableNetCore.IService;
using CrudDatatableNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDatatableNetCore.Service
{
    public class StudentService : IStudentService
    {
        private readonly DatabaseContext _context;

        public StudentService(DatabaseContext context)
        {
            _context = context;
        }
        public string Delete(int studentId)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentID == studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return "DELETE";
        }

        public Student GetById(int StudentId)
        {
            return _context.Students.SingleOrDefault(s => s.StudentID == StudentId);
        }

        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public void Save(Student oStudent)
        {
            _context.Students.Add(oStudent);
            _context.SaveChanges();
        }

        public void Update(Student oStudent)
        {
            _context.Students.Update(oStudent);
            _context.SaveChanges();
        }
    }
}
