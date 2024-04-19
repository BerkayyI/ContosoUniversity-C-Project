using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Models
{
    public class Teilnehmer
    {
        private readonly SchoolContext _db;

        public Teilnehmer(SchoolContext db)
        {
            _db = db;
        }

        public List<Student> GetStudentsInCourse(int courseID)
        {
            return _db.Students
                .Include(s => s.Enrollments)
                .Where(s => s.Enrollments.Any(e => e.CourseID == courseID))
                .ToList();
        }

        public void SearchAndDisplayResults()
        {
            Console.WriteLine("Enter search query (student name / course ID):");
            string searchQuery = Console.ReadLine();

            if (int.TryParse(searchQuery, out int courseID))
            {
                SearchAndDisplayStudentsByCourseID(courseID);
            }
            else
            {
                SearchAndDisplayStudentsByName(searchQuery);
            }
        }

        public void SearchAndDisplayStudentsByCourseID(int courseID)
        {
            Course myCourse = _db.Courses.FirstOrDefault(c => c.CourseID == courseID);

            if (myCourse != null)
            {
                Console.WriteLine($"Students enrolled in course {myCourse.Title}:");

                List<Student> studentsInCourse = _db.Students
                .Include(s => s.Enrollments) // Include the Enrollments navigation property
                .ThenInclude(e => e.Course)  // Include the Course navigation property within Enrollments
                .Where(s => s.Enrollments.Any(e => e.CourseID == courseID))
                .ToList();


                if (studentsInCourse.Count > 0)
                {
                    foreach (Student student in studentsInCourse)
                    {
                        Console.WriteLine($"{student.LastName}, {student.FirstMidName}\n");

                        Console.WriteLine("Course ID\tCourse Title\t\t\t\tCredits\tDepartment ID");
                        foreach (var enrollment in student.Enrollments)
                        {
                            Console.WriteLine($"{enrollment.CourseID}\t        {enrollment.Course.Title.PadRight(35)}{enrollment.Course.Credits.ToString().PadLeft(10)}\t        {enrollment.Course.DepartmentID}");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No students found in this course.");
                }
            }
            else
            {
                Console.WriteLine("No course found with the given ID.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public void SearchAndDisplayStudentsByName(string studentName)
        {
            List<Student> students = _db.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .Where(s => s.LastName.Contains(studentName) || s.FirstMidName.Contains(studentName))
                .ToList();

            if (students.Any())
            {
                Console.WriteLine($"Students with name '{studentName}':\n");

                foreach (Student student in students)
                {
                    Console.WriteLine($"{student.LastName}, {student.FirstMidName}\n");
                    
                    Console.WriteLine("CoursID\t\tCourses\t\t\t\t   Credits\tDepartmentID");
                    foreach (var enrollment in student.Enrollments)
                    {
                        Console.WriteLine($"{enrollment.Course.CourseID}\t        {enrollment.Course.Title.PadRight(35)}{enrollment.Course.Credits}\t        {enrollment.Course.DepartmentID}");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"No student found with name '{studentName}'.");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }


    }
}
