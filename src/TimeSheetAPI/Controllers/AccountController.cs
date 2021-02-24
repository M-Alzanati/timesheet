using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeSheetAPI.Controllers.ViewModels;
using TimeSheetAPI.Models;
using TimeSheetAPI.Services;
using Microsoft.AspNetCore.Authorization;
using log4net.Core;
using Microsoft.Extensions.Logging;

namespace TimeSheetAPI.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<MyIdentityUser> _userManager;

        private SignInManager<MyIdentityUser> _signInManager;

        private IUserService _userService;

        private ILogger<AccountController> _logger;

        public AccountController(
            UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager,
            IUserService userService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("/api/account/register")]
        public async Task<IActionResult> Register(RegisterViewModel creds)
        {
            _logger.LogInformation($"/api/account/register -> [{creds.Email}]");

            if (ModelState.IsValid && await DoRegister(creds))
            {
                return Ok("true");
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("/api/account/login")]
        public async Task<IActionResult> Login(LoginViewModel creds)
        {
            _logger.LogInformation($"/api/account/login -> [{creds.Email}]");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(creds.Email);
                if (await DoLogin(user, creds.Password))
                {
                    var token = _userService.Authenticate(
                    new AuthenticateRequest
                    {
                        Password = creds.Password,
                        Username = creds.Email
                    });
                    return Ok(token);
                }
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost("/api/account/logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"/api/account/logout");
            await _signInManager.SignOutAsync();
            return Ok();
        }

        private async Task<bool> DoLogin(MyIdentityUser user, string password)
        {
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                return result.Succeeded;
            }
            return false;
        }

        private async Task<bool> DoRegister(RegisterViewModel creds)
        {
            var user = await _userManager.FindByNameAsync(creds.Email);
            if (user != null)
            {
                user = new MyIdentityUser
                {
                    UserName = creds.Email,
                    Email = creds.Email,
                    FirstName = creds.FirstName,
                    LastName = creds.LastName
                };
                var result = await _userManager.CreateAsync(user, creds.Password);
                return result.Succeeded;
            }
            return false;
        }
    }
}
