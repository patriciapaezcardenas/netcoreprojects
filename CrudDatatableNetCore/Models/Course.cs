using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudDatatableNetCore.Models
{
    public class Course
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}