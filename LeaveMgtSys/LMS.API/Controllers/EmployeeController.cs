using LMS.API.Data;
using LMS.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDBContext _dbcontext;

        public EmployeeController(AppDBContext appDBContext)
        {
            _dbcontext = appDBContext;
        }

        public void Post([FromBody] Employee employee)
        {
            _dbcontext.Employees.Add(employee);
            _dbcontext.SaveChanges();
        }
    }
}
