using ContosoUniversity.DAL;
using System;
using ContosoUniversity.Models;
using System.Collections.Generic;
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
                Console.WriteLine("Contoso University Admininstrator Management System");
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
                        ManagaSearchEngine();
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
        private void ManagaSearchEngine()
        {
            List<Student> students = new List<Student>();
            Teilnehmer SearchEngine = new Teilnehmer();
            SearchEngine.SearchAndDisplayResults(students);
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
            // Implement logic to manage instructors (CRUD operations, etc.)
            Console.WriteLine("Manage Instructors");
        }

        private void ManageDepartments()
        {
            // Implement logic to manage departments (CRUD operations, etc.)
            Console.WriteLine("Manage Departments");
        }

    }
}