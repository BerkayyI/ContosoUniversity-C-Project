using ContosoUniversity.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public string Email { get; set; }

        public void ViewAllStudents(SchoolContext dbContext)
        {
            Console.WriteLine("All Students:\n");
            Console.WriteLine("ID\tLast Name\tFirst Name\tEnrollment Date\t\tBirthDate\tEmail\n");
            foreach (var student in dbContext.Students)
            {
                Console.WriteLine($"{student.ID}\t{student.LastName.PadRight(15)}\t{student.FirstMidName.PadRight(12)}\t{student.EnrollmentDate.ToString("dd/MM/yyyy")}\t\t{student.BirthDate.ToString("dd/MM/yyyy")}\t{student.Email}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public void AddStudent(SchoolContext dbContext)
        {
            Console.WriteLine("Enter student's last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter student's first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter student's enrollment date (yyyy-MM-dd):");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime enrollmentDate))
            {
                Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                return;
            }

            Console.WriteLine("Enter student's birth date (yyyy-MM-dd):");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDate))
            {
                Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                return;
            }

            Console.WriteLine("Enter student's Email:");
            string email = Console.ReadLine();

            List<Enrollment> enrollments = new List<Enrollment>();

            while (true)
            {
                Console.WriteLine("Enter course ID (or type 'done' to finish adding courses):");
                string input = Console.ReadLine();
                if (input.ToLower() == "done")
                    break;

                if (!int.TryParse(input, out int courseId))
                {
                    Console.WriteLine("Invalid input for course ID. Please enter a number.");
                    continue;
                }

                var course = dbContext.Courses.FirstOrDefault(c => c.CourseID == courseId);
                if (course == null)
                {
                    Console.WriteLine($"Course with ID {courseId} not found.");
                    continue;
                }

                enrollments.Add(new Enrollment { CourseID = courseId });
            }

            var newStudent = new Student
            {
                LastName = lastName,
                FirstMidName = firstName,
                EnrollmentDate = enrollmentDate,
                BirthDate = birthDate,
                Email = email,
                Enrollments = enrollments
            };

            dbContext.Students.Add(newStudent);
            dbContext.SaveChanges();
            Console.WriteLine("Student added successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public void UpdateStudent(SchoolContext dbContext)
        {
            Console.WriteLine("Enter student ID to update:");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid student ID. Please enter a number.");
                return;
            }

            var studentToUpdate = dbContext.Students.Find(studentId);
            if (studentToUpdate == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.WriteLine("Enter student's new last name (or press Enter to skip):");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                studentToUpdate.LastName = lastName;
            }

            Console.WriteLine("Enter student's new first name (or press Enter to skip):");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                studentToUpdate.FirstMidName = firstName;
            }

            Console.WriteLine("Enter student's new enrollment date (yyyy-MM-dd) (or press Enter to skip):");
            string enrollmentDateString = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(enrollmentDateString))
            {
                if (!DateTime.TryParse(enrollmentDateString, out DateTime enrollmentDate))
                {
                    Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                    return;
                }
                studentToUpdate.EnrollmentDate = enrollmentDate;
            }

            dbContext.SaveChanges();
            Console.WriteLine("Student updated successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public void DeleteStudent(SchoolContext dbContext)
        {
            Console.WriteLine("Enter student ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid student ID. Please enter a number.");
                return;
            }

            var studentToDelete = dbContext.Students.Find(studentId);
            if (studentToDelete == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            dbContext.Students.Remove(studentToDelete);
            dbContext.SaveChanges();
            Console.WriteLine("Student deleted successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
