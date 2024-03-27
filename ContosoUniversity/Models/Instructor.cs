using ContosoUniversity.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime HireDate { get; set; }
        public int HourlyPay { get; set; }
        // maybe implement this idea also of hourly pay!

        public void ViewAllInstructors(SchoolContext dbContext)
        {
            Console.WriteLine("All Instructors:\n");
            Console.WriteLine("ID\tLast Name\tFirst Name\tHire Date\t Hourly Pay\n");
            foreach (var instructor in dbContext.Instructors)
            {
                Console.WriteLine($"{instructor.ID}\t{instructor.LastName}\t        {instructor.FirstMidName}\t        {instructor.HireDate.ToString("dd/MM/yyyy")}\t {instructor.HourlyPay}$");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
        
        public void AddInstructor(SchoolContext dbContext)
        {
            Console.WriteLine("Enter instructor's last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter instructor's first name:");
            string firstName = Console.ReadLine();

            int hourlypay;
            Console.WriteLine("Enter instructor's hourly pay amount ($):");
            if (!int.TryParse(Console.ReadLine(), out hourlypay))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a number.");
                return;
            }

            Console.WriteLine("Enter instructor's hire date (yyyy-MM-dd):");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime hiredate))
            {
                Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                return;
            }

            var newInsturctor = new Instructor
            {
                LastName = lastName,
                FirstMidName = firstName,
                HireDate = hiredate,
                HourlyPay = hourlypay
            };

            dbContext.Instructors.Add(newInsturctor);
            dbContext.SaveChanges();
            Console.WriteLine("Instructor added successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();

        }

        public void UpdateInstructor(SchoolContext dbContext)
        {
            Console.WriteLine("Enter Instructor ID to update:");
            if (!int.TryParse(Console.ReadLine(), out int instructorid))
            {
                Console.WriteLine("Invalid student ID. Please enter a number.");
                return;
            }
            var instructortoupdate = dbContext.Instructors.Find(instructorid);
            if (instructortoupdate == null) 
            {
                Console.WriteLine("Instructor not found.");
                return;
            }

            Console.WriteLine("Enter instructor's new last name (or press Enter to skip):");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                instructortoupdate.LastName = lastName;
            }

            Console.WriteLine("Enter instructor's new first name (or press Enter to skip):");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                instructortoupdate.FirstMidName = firstName;
            }

            Console.WriteLine("Enter Instructor's new Hire date (yyyy-MM-dd) (or press Enter to skip):");
            string HireDateStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(HireDateStr))
            {
                if (!DateTime.TryParse(HireDateStr, out DateTime hiredate))
                {
                    Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                    return;
                }
                instructortoupdate.HireDate = hiredate;
            }
            dbContext.SaveChanges();
            Console.WriteLine("Instructor updated successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public void DeleteInstructor(SchoolContext dbContext)
        {
            Console.WriteLine("Enter Instructor ID to update:");
            if (!int.TryParse(Console.ReadLine(), out int instructorid))
            {
                Console.WriteLine("Invalid student ID. Please enter a number.");
                return;
            }
            var instructortodelete = dbContext.Instructors.Find(instructorid);
            if (instructortodelete == null)
            {
                Console.WriteLine("Instructor not found.");
                return;
            }

            dbContext.Instructors.Remove(instructortodelete);
            dbContext.SaveChanges();
            Console.WriteLine("Instructor deleted successfully.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
