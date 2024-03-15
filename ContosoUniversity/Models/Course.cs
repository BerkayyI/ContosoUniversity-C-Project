using ContosoUniversity.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }

        public void ViewAllCourses(SchoolContext dbContext)
        {
            Console.WriteLine("All Courses:\n");
            Console.WriteLine("ID\tTitle\t\t\t\tCredits\t  DepartmentID\n");
            foreach (var course in dbContext.Courses)
            {
                Console.WriteLine($"{course.CourseID}\t{course.Title.PadRight(35)}{course.Credits}\t      {course.DepartmentID}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public void AddCourse(SchoolContext dbContext)
        {
            Console.WriteLine("Enter course title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter credits:");
            if (!int.TryParse(Console.ReadLine(), out int credits))
            {
                Console.WriteLine("Invalid input for credits. Please enter a number.");
                return;
            }

            Console.WriteLine("Enter department ID:");
            if (!int.TryParse(Console.ReadLine(), out int departmentId))
            {
                Console.WriteLine("Invalid input for department ID. Please enter a number.");
                return;
            }

            // Create new course object
            var newCourse = new Course
            {
                Title = title,
                Credits = credits,
                DepartmentID = departmentId
            };

            // Add course to database
            dbContext.Courses.Add(newCourse);
            dbContext.SaveChanges();
            Console.WriteLine("Course added successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public void UpdateCourse(SchoolContext dbContext)
        {
            Console.WriteLine("Enter course ID to update:");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid course ID. Please enter a number.");
                return;
            }

            // Find course by ID
            var courseToUpdate = dbContext.Courses.Find(courseId);
            if (courseToUpdate == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.WriteLine("Enter new title (or press Enter to skip):");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                courseToUpdate.Title = newTitle;
            }

            Console.WriteLine("Enter new credits (or press Enter to skip):");
            string creditsInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(creditsInput))
            {
                if (!int.TryParse(creditsInput, out int newCredits))
                {
                    Console.WriteLine("Invalid input for credits. Please enter a number.");
                    return;
                }
                courseToUpdate.Credits = newCredits;
            }

            Console.WriteLine("Enter new department ID (or press Enter to skip):");
            string departmentIdInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(departmentIdInput))
            {
                if (!int.TryParse(departmentIdInput, out int newDepartmentId))
                {
                    Console.WriteLine("Invalid input for department ID. Please enter a number.");
                    return;
                }
                courseToUpdate.DepartmentID = newDepartmentId;
            }

            // Update course in database
            dbContext.SaveChanges();
            Console.WriteLine("Course updated successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public void DeleteCourse(SchoolContext dbContext)
        {
            Console.WriteLine("Enter course ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid course ID. Please enter a number.");
                return;
            }

            // Find course by ID
            var courseToDelete = dbContext.Courses.Find(courseId);
            if (courseToDelete == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            // Remove course from database
            dbContext.Courses.Remove(courseToDelete);
            dbContext.SaveChanges();
            Console.WriteLine("Course deleted successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
