using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Models
{
    public class Leave
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string LeaveReason { get; set; }
        public bool IsApproved { get; set; }
        public int UserId { get; set; }
    }
}
