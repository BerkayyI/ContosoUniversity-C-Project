using ContosoUniversity.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ContosoUniversity.Models
{
    public class Student
    {
        
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Course> EnrolledCourses { get; set; }
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
            string Email = Console.ReadLine();


            List<Course> enrolledCourses = new List<Course>();

            // Prompt user to enter course details
            while (true)
            {
                Console.WriteLine("Enter course title (or type 'done' to finish adding courses):");
                string courseTitle = Console.ReadLine();
                if (courseTitle.ToLower() == "done")
                    break;

                Console.WriteLine("Enter credits:");
                if (!int.TryParse(Console.ReadLine(), out int credits))
                {
                    Console.WriteLine("Invalid input for credits. Please enter a number.");
                    continue;
                }

                Console.WriteLine("Enter department ID:");
                if (!int.TryParse(Console.ReadLine(), out int departmentId))
                {
                    Console.WriteLine("Invalid input for department ID. Please enter a number.");
                    continue;
                }

                // Retrieve or create the course
                Course course = RetrieveOrCreateCourse(dbContext, courseTitle, credits, departmentId);
                enrolledCourses.Add(course);
            }
            
            // Create new student object
            var newStudent = new Student
            {
                LastName = lastName,
                FirstMidName = firstName,
                EnrollmentDate = enrollmentDate,
                BirthDate = birthDate,
                Email = Email,
                EnrolledCourses = enrolledCourses
            };



            // Add student to database
            dbContext.Students.Add(newStudent);
            dbContext.SaveChanges();
            Console.WriteLine("Student added successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        private Course RetrieveOrCreateCourse(SchoolContext dbContext, string courseTitle, int credits, int departmentId)
        {
            // Check if the course exists in the database
            Course existingCourse = dbContext.Courses.FirstOrDefault(c => c.Title == courseTitle && c.Credits == credits && c.DepartmentID == departmentId);

            if (existingCourse != null)
            {
                return existingCourse;
            }
            else
            {
                // Create a new course if it doesn't exist
                var newCourse = new Course
                {
                    Title = courseTitle,
                    Credits = credits,
                    DepartmentID = departmentId
                    // Add other properties for the course if needed...
                };

                // Add the new course to the database
                dbContext.Courses.Add(newCourse);
                dbContext.SaveChanges();

                return newCourse;
            }
        }

        public void UpdateStudent(SchoolContext dbContext)
        {
            Console.WriteLine("Enter student ID to update:");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid student ID. Please enter a number.");
                return;
            }

            // Find student by ID
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

            // Update student in database
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

            // Find student by ID
            var studentToDelete = dbContext.Students.Find(studentId);
            if (studentToDelete == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            // Remove student from database
            dbContext.Students.Remove(studentToDelete);
            dbContext.SaveChanges();
            Console.WriteLine("Student deleted successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }


    }

    

}