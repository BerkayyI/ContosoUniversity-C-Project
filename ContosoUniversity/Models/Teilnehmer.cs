using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContosoUniversity.DAL;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Models
{
    public class Teilnehmer
    {
        public int ID { get; set; }
        public Course course { get; set; }
        public Student student { get; set; }

        
        private List<Student> students = new List<Student>();

        // Method to search for students by name or courses by ID or name and display the results in the console
        public void SearchAndDisplayResults(List<Student> students)
        {
            Console.WriteLine("Enter search query (student name / course ID / course name):");
            string searchQuery = Console.ReadLine();

            if (int.TryParse(searchQuery, out int courseID))
            {
                // Search by course ID
                SearchAndDisplayStudentsByCourseID(students, courseID);
            }
            else
            {
                // Assume it's either student name or course name
                SearchAndDisplayStudentsOrCoursesByName(students, searchQuery);
            }
        }

        // Method to search for students by course ID and display the results
        private void SearchAndDisplayStudentsByCourseID(List<Student> students, int courseID)
        {
            Course course = GetCourseByID(students, courseID);
            if (course != null)
            {
                List<Student> enrolledStudents = students.Where(s => s.EnrolledCourses.Contains(course)).ToList();
                if (enrolledStudents.Any())
                {
                    Console.WriteLine($"Students enrolled in the course '{course.Title}' (ID: {course.CourseID}):");
                    foreach (var student in enrolledStudents)
                    {
                        Console.WriteLine($"Name: {student.FirstMidName}, Birthdate: {student.BirthDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine($"No students found for course '{course.Title}' (ID: {course.CourseID}).");
                }
            }
            else
            {
                Console.WriteLine($"No course found with ID '{courseID}'.");
            }
        }

        // Method to search for students or courses by name and display the results
        private void SearchAndDisplayStudentsOrCoursesByName(List<Student> students, string name)
        {
            // Search for students by name
            List<Student> matchingStudents = students.Where(s => s.FirstMidName.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchingStudents.Any())
            {
                Console.WriteLine($"Students with name '{name}':");
                foreach (var student in matchingStudents)
                {
                    Console.WriteLine($"Name: {student.FirstMidName}, Birthdate: {student.BirthDate.ToShortDateString()}");
                }
            }
            else
            {
                // Search for courses by name
                Course matchingCourse = students.SelectMany(s => s.EnrolledCourses).FirstOrDefault(c => c.Title.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (matchingCourse != null)
                {
                    List<Student> enrolledStudents = students.Where(s => s.EnrolledCourses.Contains(matchingCourse)).ToList();
                    if (enrolledStudents.Any())
                    {
                        Console.WriteLine($"Students enrolled in the course '{matchingCourse.Title}':");
                        foreach (var student in enrolledStudents)
                        {
                            Console.WriteLine($"Name: {student.FirstMidName}, Birthdate: {student.BirthDate.ToShortDateString()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No students found for course '{matchingCourse.Title}'.");
                    }
                }
                else
                {
                    Console.WriteLine($"No students or courses found with the name '{name}'.");
                }
            }
        }

        // Method to find a course by ID
        private Course GetCourseByID(List<Student> students, int courseID)
        {
            return students.SelectMany(s => s.EnrolledCourses).FirstOrDefault(c => c.CourseID == courseID);
        }

    }
}
