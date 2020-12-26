using AccountMgmt.DB;
using AccountMgmt.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmt.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        Repository _repository;

        public AccountController(Repository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(string account_id)
        {
            var bal = await _repository.GetBalance(account_id);
            if (bal.HasValue)
                return Ok(bal.Value);
            else
                return NotFound();
        }

        [HttpPost("amount")]
        public async Task<IActionResult> PostAmount([FromBody]TransactionModel transaction)
        {
            await _repository
        }
    }
}
