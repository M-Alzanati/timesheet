using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using TimeSheetAPI.Controllers.ViewModels;
using TimeSheetAPI.Models;

namespace TimeSheetAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private ILogger<TimeSheetController> _logger;

        private IUserLoginRepository _repo;

        public TimeSheetController(ILogger<TimeSheetController> logger, IUserLoginRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        /// <summary>
        /// Add user login
        /// </summary>
        /// <param name="model">{uuid, time}</param>
        /// <returns>{boolean}</returns>
        [HttpPost("/api/timeSheet/logins/add")]
        public async Task<IActionResult> AddLogin([FromBody] LogTimeViewModel model)
        {
            _logger.LogInformation($"/api/timeSheet/logins/add");
            try
            {
                var time = DateTime.Parse(model.Time);
                await _repo.SaveUserLoginAsync(model.UUId, time);
                return Ok("true");
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/logins/add", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Add user logout
        /// </summary>
        /// <param name="model">{uuid, time}</param>
        /// <returns>{boolean}</returns>
        [HttpPost("/api/timeSheet/logouts/add")]
        public async Task<IActionResult> AddLogout([FromBody] LogTimeViewModel model)
        {
            _logger.LogInformation($"/api/timeSheet/logouts/add");
            try
            {
                var time = DateTime.Parse(model.Time);
                await _repo.SaveUserLogoutAsync(model.UUId, time);
                return Ok("true");
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/logouts/add", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// get first login
        /// </summary>
        /// <param name="uuid">{user_unique_id}</param>
        /// <returns>{time}</returns>
        [HttpGet("/api/timeSheet/logins/first/{uuid}")]
        public async Task<IActionResult> GetFirstLogin([FromRoute] string uuid)
        {
            _logger.LogInformation($"/api/timeSheet/logins/first/{uuid}");
            var result = await _repo.GetFirstLogin(uuid);
            return Ok(new { time = result });
        }

        /// <summary>
        /// get last logout
        /// </summary>
        /// <param name="uuid">{user_unique_id}</param>
        /// <returns>{time}</returns>
        [HttpGet("/api/timeSheet/logouts/last/{uuid}")]
        public async Task<IActionResult> GetLastLogout([FromRoute] string uuid)
        {
            _logger.LogInformation($"/api/timeSheet/logouts/last/{uuid}");
            var result = await _repo.GetLastLogout(uuid);
            return Ok(new { time = result });
        }

        /// <summary>
        /// Get today's logins
        /// </summary>
        /// <param name="uuid">{user_unique_id}</param>
        /// <returns>[{time},{time}]</returns>
        [HttpGet("/api/timeSheet/logins/today/{uuid}")]
        public async Task<IActionResult> GetTodaysLogins([FromRoute] string uuid)
        {
            _logger.LogInformation($"/api/timeSheet/logins/today/{uuid}");
            try
            {
                var lst = await _repo.GetUserLogins(uuid);
                return Ok(new { times = lst.Where(r => r.LoginTime.Date == DateTime.Now.Date) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/logins/today/{uuid}", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get today's logouts
        /// </summary>
        /// <param name="uuid">{user_unique_id}</param>
        /// <returns>[{time},{time}]</returns>
        [HttpGet("/api/timeSheet/logouts/today/{uuid}")]
        public async Task<IActionResult> GetTodaysLogouts([FromRoute] string uuid)
        {
            _logger.LogInformation($"/api/timeSheet/logouts/today/{uuid}");
            try
            {
                var lst = await _repo.GetUserLogouts(uuid);
                return Ok(new { times = lst.Where(r => r.LogoutTime.Date == DateTime.Now.Date) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/logouts/today/{uuid}", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Submit login/logut per day, each day contains only one record
        /// </summary>
        /// <param name="model">{date, login, logout, uuid}</param>
        /// <returns>{boolean}</returns>
        [HttpPost("/api/timeSheet/register")]
        public async Task<IActionResult> RegisterTimeSheet([FromBody] SubmitSheetViewModel model)
        {
            _logger.LogInformation($"/api/timeSheet/register");
            try
            {
                var res = await _repo.SaveOrUpdateTimeSheetAsync(model.UUId, model.Date, model.LoginTime, model.LogoutTime);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/register", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get user timesheet
        /// </summary>
        /// <param name="uuid">{user_unique_id}</param>
        /// <returns>[{date, login, logout}, {date, login, logout}]</returns>
        [HttpGet("/api/timesheet/{uuid}")]
        public async Task<IActionResult> GetTimeSheet([FromRoute] string uuid)
        {
            _logger.LogInformation($"/api/timeSheet/{uuid}");
            try
            {
                var res = await _repo.GetSubmissionSheets(uuid);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/{uuid}", ex);
                return BadRequest();
            }
        }
    }
}
