using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Models
{
    public class Employee
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public int DepartmentId { get; set; }
        public bool IsActive { get; set; }
    }
}
