using System;
using System.Threading.Tasks;
using System.Linq;
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

        [HttpPost("/api/timeSheet/logins/add")]
        public async Task<IActionResult> AddLogin([FromBody] LogTimeViewModel model)
        {
            _logger.LogInformation($"/api/timeSheet/logins/add");
            try
            {
                await _repo.SaveUserLoginAsync(model.UUId, model.Time);
                return Ok("true");
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/logins/add", ex);
                return BadRequest();
            }
        }

        [HttpPost("/api/timeSheet/logouts/add")]
        public async Task<IActionResult> AddLogout([FromBody] LogTimeViewModel model)
        {
            _logger.LogInformation($"/api/timeSheet/logouts/add");
            try
            {
                await _repo.SaveUserLogoutAsync(model.UUId, model.Time);
                return Ok("true");
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/logouts/add", ex);
                return BadRequest();
            }
        }

        [HttpGet("/api/timeSheet/logins/{uuid}")]
        public async Task<IActionResult> GetLogins([FromRoute] string uuid)
        {
            _logger.LogInformation($"/api/timeSheet/logins/{uuid}");
            try
            {
                await _repo.GetUserLogins(uuid);
                return Ok("true");
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/logins/{uuid}", ex);
                return BadRequest();
            }
        }

        [HttpGet("/api/timeSheet/logouts/{uuid}")]
        public async Task<IActionResult> GetLogouts([FromRoute] string uuid)
        {
            _logger.LogInformation($"/api/timeSheet/logins/{uuid}");
            try
            {
                var result = await _repo.GetUserLogouts(uuid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"/api/timeSheet/logins/{uuid}", ex);
                return BadRequest();
            }
        }
    }
}
