using ListaKontaktow.BusinessLayer.Services;
using ListaKontaktow.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ListaKontaktow.Controllers
{
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        const int minPasswordLength = 8;
        const string passwordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@#$%^&+=]).*$";

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("{email}")]
        public IActionResult CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }
            if (!_loginService.IsValidEmail(email))
            {
                return BadRequest("Invalid Email format.");
            }
            if (_loginService.IsDuplicateEmail(email))
            {
                return Conflict("Email already exists.");
            }
            return Ok("Email is valid and unique.");
        }

        [HttpGet("{email}")]
        public IActionResult CheckPassword(string password)
        {
            
            if (string.IsNullOrEmpty(password))
            {
                return BadRequest("Password is required.");
            }
            if (password.Length < minPasswordLength)
            {
                return BadRequest("Password should be at least 8 characters long.");
            }
            if (!Regex.IsMatch(password, passwordPattern))
            {
                return BadRequest("Password should contain at least one lowercase letter, an uppercase letter, a digit and a special character.");
            }
            return Ok("Password meets complexity standards");
        }

        //Method: POST
        //URL: http://localhost:10500/api/login
        //Body:
        [HttpPost]
        public void AddUser([FromBody] User user)
        {
            _loginService.AddUser(user);
        }

        //Method: GET
        //URL: http://localhost:10500/api/login/find?email={email}&password={password}
        //Body:
        [HttpGet("find")]
        public Person GetLoginAccess([FromQuery] string email, [FromQuery] string password)
        {
            return _loginService.GetLoginAccess(email, password);
        }
    }
}
