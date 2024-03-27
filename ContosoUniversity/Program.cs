using ContosoUniversity.DAL;
using System;
using ContosoUniversity.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace ContosoUniversity
{
    public class Program
    {
        private SchoolContext dbContext = new SchoolContext();

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Main_Menu();
        }

        public void Main_Menu()
        {
            while (true)
            {
                Console.WriteLine("\nContoso University Admininstrator Management System");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1. Manage Students");
                Console.WriteLine("2. Manage Courses");
                Console.WriteLine("3. Manage Instructors");
                Console.WriteLine("4. Manage Departments");
                Console.WriteLine("5. Search Engine");
                Console.WriteLine("6. Exit");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        ManageStudents();
                        break;
                    case 2:
                        Console.Clear();
                        ManageCourses();
                        break;
                    case 3:
                        Console.Clear();
                        ManageInstructors();
                        break;
                    case 4:
                        Console.Clear();
                        ManageDepartments();
                        break;
                    case 5:
                        Console.Clear();
                        
                        Teilnehmer teilnehmer = new Teilnehmer(dbContext);
                        SearchStudents(teilnehmer);
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Exiting the program...");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void SearchStudents(Teilnehmer teilnehmer)
        {
            Console.WriteLine("Choose search option:");
            Console.WriteLine("1. Search by course ID");
            Console.WriteLine("2. Search by student name\n");
            Console.Write("Enter your choice: ");

            string choice;
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "1" || choice == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1 or 2.");
                }
            }

            switch (choice)
            {
                case "1":
                    int courseId;
                    while (true)
                    {
                        Console.Write("\nEnter course ID: ");
                        if (int.TryParse(Console.ReadLine(), out courseId))
                        {
                            // Search for students by course ID
                            teilnehmer.SearchAndDisplayStudentsByCourseID(courseId);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input. Please enter a valid course ID.");
                        }
                    }
                    break;

                case "2":
                    Console.Write("\nEnter student name: ");
                    string studentName = Console.ReadLine();

                    // Search for students by name
                    teilnehmer.SearchAndDisplayStudentsByName(studentName);
                    break;
            }
        }


        private void ManageStudents()
        {
            Student student = new Student();
           
            while (true)
            {
                Console.WriteLine("Student Management");
                Console.WriteLine("------------------");
                Console.WriteLine("1. View All Students");
                Console.WriteLine("2. Add Student");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Back to Main Menu");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        student.ViewAllStudents(dbContext);
                        break;
                    case 2:
                        Console.Clear();
                        student.AddStudent(dbContext);
                        break;
                    case 3:
                        Console.Clear();
                        student.UpdateStudent(dbContext);
                        break;
                    case 4:
                        Console.Clear();
                        student.DeleteStudent(dbContext);
                        break;
                    case 5:
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();  
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ManageCourses()
        {
            Course course = new Course();
            while (true)
            {
                Console.WriteLine("Manage Courses");
                Console.WriteLine("1. View All Courses");
                Console.WriteLine("2. Add Course");
                Console.WriteLine("3. Update Course");
                Console.WriteLine("4. Delete Course");
                Console.WriteLine("5. Back to Main Menu");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        course.ViewAllCourses(dbContext);
                        break;
                    case 2:
                        Console.Clear();
                        course.AddCourse(dbContext);
                        break;
                    case 3:
                        Console.Clear();
                        course.UpdateCourse(dbContext);
                        break;
                    case 4:
                        Console.Clear();
                        course.DeleteCourse(dbContext);
                        break;
                    case 5:
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ManageInstructors()
        {
            Instructor instructor = new Instructor();

            while (true)
            {
                Console.WriteLine("Instructor Management");
                Console.WriteLine("---------------------");
                Console.WriteLine("1. View All Instructors");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Update Instructor");
                Console.WriteLine("4. Delete Instructor");
                Console.WriteLine("5. Back to Main Menu");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        instructor.ViewAllInstructors(dbContext);
                        break;
                    case 2:
                        Console.Clear();
                        instructor.AddInstructor(dbContext);
                        break;
                    case 3:
                        Console.Clear();
                        instructor.UpdateInstructor(dbContext);
                        break;
                    case 4:
                        Console.Clear();
                        instructor.DeleteInstructor(dbContext);
                        break;
                    case 5:
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ManageDepartments()
        {
            // Implement logic to manage departments (CRUD operations, etc.)
            Console.WriteLine("Manage Departments");
        }

    }
}