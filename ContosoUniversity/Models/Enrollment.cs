using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public string Grade { get; set; }
    }
}
