 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Models
{
    public class Log
    {

        [Key]
        public int LogId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        
    }
}
