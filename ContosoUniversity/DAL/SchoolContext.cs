using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;

namespace ContosoUniversity.DAL
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(s => s.ID);
            modelBuilder.Entity<Course>().HasKey(c => c.CourseID);
            modelBuilder.Entity<Instructor>().HasKey(c => c.ID);
            modelBuilder.Entity<Department>().HasKey(c => c.DepartmentID);
            modelBuilder.Entity<OfficeAssignment>().HasKey(c => c.InstructorID);
            modelBuilder.Entity<CourseAssignment>().HasKey(c => c.CourseID);
            modelBuilder.Entity<Enrollment>().HasKey(c => c.EnrollmentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID)
                .IsRequired();

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentID)
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var dbPath = @"C:\Users\benbe\source\repos\ContosoUniversity-C-Project\ContosoUniversity\db\school.db";
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }

            base.OnConfiguring(optionsBuilder);
        }


    }
}
