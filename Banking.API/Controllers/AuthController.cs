using Banking.API.Domain;
using Banking.API.Models.Account;
using Banking.API.Models.Auth;
using Banking.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        #region Private Varaibles

        private readonly IMemoryCache _cache;
        private readonly AccountService _accountService;

        #endregion

        #region Constructor

        public AuthController(IMemoryCache cache, AccountService accountService)
        {
            _cache = cache;
            _accountService = accountService;
        }

        #endregion

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var exists = _cache.TryGetValue(request.Email, out AccountViewModel viewModel);

            if (!exists)
                return BadRequest("Account does not exist for given email.");
            else if (request.Password != viewModel.User.Password)
                return BadRequest("Incorrect password was provided.");

            return Ok(viewModel);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (_cache.Get(request.Email) != null)
            {
                return BadRequest("Email provided is already in use.");
            }

            // Password would not be in view model in real application. 
            var accountViewModel = new AccountViewModel()
            {
                User = new User
                {
                    Email = request.Email,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                },
                Balance = 0
            };

            _cache.Set(request.Email, accountViewModel, new DateTimeOffset(DateTime.Now.AddHours(1)));

            return Ok(accountViewModel);
        }
    }
}
