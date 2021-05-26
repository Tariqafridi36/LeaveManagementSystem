using LMS.API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LMS.API.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Log> Logs { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>().HasData(
            new Department
            {
                DepartmentId = 1,
                DepartmentName = "IT",
                CreatedOn = DateTime.Today
            }
        );

            modelBuilder.Entity<Employee>().HasData(
              new Employee
              {
                  Id = 1,
                  DepartmentId = 1,
                  Email = "peter@gmail.com",
                  Name = "Peter",
                  Address = "v-11",
                  Designation = "Manager",
                  ContactNo = "009713454322",
                  IsActive = true,
                  CreatedOn = DateTime.Today
              }
          );
            modelBuilder.Entity<AppUser>().HasData(
             new AppUser
             {
                 UserId = 1,
                 Email = "peter@gamil.com",
                 Password = "Peter@123",
                 UserName = "peter",
                 IsActive = true,
                 CreatedOn = DateTime.Today
             }
         ); 

        }
        #endregion
    }
}
