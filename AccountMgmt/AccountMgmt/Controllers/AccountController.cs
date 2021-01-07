using AccountMgmt.DB;
using AccountMgmt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
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
        
        [HttpGet("balance/{account_id}")]
        public async Task<IActionResult> GetBalance(string account_id)
        {
            var bal = await _repository.GetBalance(account_id);
            if (bal.HasValue)
                return Ok(new { balance = bal });
            else
                return NotFound();
        }

        [HttpPost("amount")]
        public async Task<IActionResult> PostAmount([FromHeader(Name = "Transaction-Id")]Guid? transaction_id, [FromBody]TransactionModel transaction)
        {
            if (!transaction_id.HasValue)
                return BadRequest();
            try
            {
                await _repository.PostAmount(transaction_id.Value, transaction);
            }
            catch (SqliteException e)
            {
                if (e.SqliteErrorCode == 19)
                    return Ok();
                throw;
            }
            return Ok();
        }

        [HttpGet("transaction/{transaction_id}")]
        public async Task<IActionResult> GetTransaction(string transaction_id)
        {
            var tr = await _repository.GetTransaction(transaction_id);
            if (tr != null)
                return Ok(new { transaction_id = transaction_id, account_id = tr.AccountId, amount = tr.Balance });
            else
                return NotFound();
        }

        [HttpGet("max_transaction_volume")]
        public async Task<IActionResult> GetTransactionVolume()
        {
            var accts = await _repository.GetTransactionVolume();
            if (accts == null)
                return NotFound();
            else
                return Ok(accts);
        }
    }
}
