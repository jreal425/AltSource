using Banking.API.Models.Account;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.API.Services
{
    public class AccountService
    {
        private readonly IMemoryCache _cache;

        public AccountService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void GetOrCreateAccountInformation(string email)
        {
            var info = _cache.GetOrCreate(email, entry =>
            {
                entry.AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddHours(1));
                return new AccountViewModel();
            });
        }
    }
}
