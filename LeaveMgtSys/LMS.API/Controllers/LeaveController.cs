using LMS.API.Data;
using LMS.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LMS.API.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
 

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController] 
    public class LeaveController : ControllerBase
    {
        private readonly AppDBContext _dbcontext;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<LeaveController> _logger;
        private readonly IBaseResponse _response;
        int UserId = 0; 

        //Inject AppDBContext class here
        public LeaveController(AppDBContext dBContext, IHttpContextAccessor httpContext, ILogger<LeaveController> logger, IBaseResponse response)
        {
            _dbcontext = dBContext;
            _httpContext = httpContext;
            _logger = logger;
            _response = response;
            try { UserId = Convert.ToInt32(_httpContext.HttpContext.User.Claims.ToList()[0].Value); } catch (Exception) { }
        }

        [HttpGet("GetAllLeaves")]
        public async Task<IBaseResponse> GetAllLeaves()
        {
            try
            {
                _response.data = await _dbcontext.Leaves.Where(x => x.UserId == UserId).ToListAsync();
                _response.desc = "Found.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                _response.code = "01"; 
            }

            return  _response;
        }


        [HttpGet("GetUserLeaveForDashboard")]
        public async Task<IBaseResponse> GetUserLeaveForDashboard()
        {
            try
            {
                var UserIdParam = new SqlParameter("@USER_ID", UserId); 
                _response.data = await _dbcontext.Leaves.FromSqlRaw("exec GET_LEAVE_DATA @USER_ID", UserIdParam).Select(x => new { Count = x.Id, Reason = x.LeaveReason }).ToListAsync();
                if (_response.data == null)
                {
                    _response.code = "01";
                    _response.desc = "No data found.";
                }
            }
            catch (Exception ex)
            {
                _response.code = "01";
                _logger.LogError(ex, ex.Message, null);
            }
            return _response;
        }

        [HttpPost("addLeave")]
        public async Task<IBaseResponse> addLeave([FromBody] Leave leave)
        {
            try
            {
                leave.UserId = UserId;
                await _dbcontext.Leaves.AddAsync(leave);
                await _dbcontext.SaveChangesAsync();
                var inserted = _dbcontext.Leaves.Where(x => x.Title == leave.Title && x.FromDate == leave.FromDate && x.ToDate == leave.ToDate).FirstOrDefault();
                if (inserted != null)
                { 
                    _response.desc = "Your leave application has been submitted.";
                }
                else
                {
                    _response.code = "01"; 
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message.ToString());
                _response.code = "01";
                _response.desc = ex.Message.ToString();
            }
            return _response;
        }


        [HttpPost("UpdateLeave")]
        public async Task<IBaseResponse> UpdateLeave([FromBody] Leave leave)
        {
            try
            {
                var entity = _dbcontext.Leaves.Where(x => x.Id == leave.Id).FirstOrDefault();
                if (entity != null)
                {
                    entity.Title = leave.Title;
                    entity.FromDate = leave.FromDate;
                    entity.ToDate = leave.ToDate;
                    entity.LeaveReason = leave.LeaveReason; 
                    await _dbcontext.SaveChangesAsync(); 
                    
                    _response.desc = "Your leave has been updated.";
                } 
            }
            catch (Exception ex)
            {
                _response.code = "01";
                _logger.LogError(ex.Message.ToString());
            }
            return _response;
        }


        [HttpDelete("{id}")]
        public async Task<IBaseResponse> Delete(int id)
        {
            try
            {
                var deleteItem = await _dbcontext.Leaves.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (deleteItem != null)
                {
                    _dbcontext.Leaves.Remove(deleteItem);
                    _dbcontext.SaveChanges(); 
                    _response.desc = "Leave has been deleted.";
                }
            }
            catch (Exception ex)
            {
                _response.code = "01";
                _logger.LogError(ex.Message.ToString());
            }

            return _response;
        }
    }
}
