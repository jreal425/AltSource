using Banking.API.Domain;
using Banking.API.Models.Account;
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
    [Route("api/account")]
    public class AccountController : Controller
    {
        #region Private Varaibles

        private readonly IMemoryCache _cache;
        private readonly TransactionService _service;

        #endregion

        #region Constructor

        public AccountController(IMemoryCache cache, TransactionService service)
        {
            _cache = cache;
            _service = service;
        }

        #endregion

        [HttpGet]
        public IActionResult GetSavings(string email)
        {
            var exists = _cache.TryGetValue(email, out AccountViewModel viewModel);
            if (!exists)
                return BadRequest("Account does not exist for given email.");

            return Ok(viewModel.History.OrderByDescending(i => i.Date));
        }

        [HttpPost]
        public IActionResult PostTransaction([FromBody] TransactionRequest request)
        {
            var exists = _cache.TryGetValue(request.Email, out AccountViewModel viewModel);
            if (!exists)
                return BadRequest("Account does not exist for given email.");

            var transaction = _service.ProcessTransaction(request, viewModel.Balance);

            viewModel.History.Add(transaction);
            viewModel.Balance = transaction.Balance;

            _cache.Set(viewModel.User.Email, viewModel);

            return Ok(transaction);
        }
    }
}
