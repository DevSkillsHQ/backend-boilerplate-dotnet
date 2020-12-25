using AccountMgmt.DB;
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
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(string account_id)
        {
            var bal = await Repository.GetBalance(account_id);
            if (bal.HasValue)
                return Ok(bal.Value);
            else
                return NotFound();
        }
    }
}
