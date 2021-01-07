using AccountMgmt.Models;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmt.DB
{
    public class Repository: IDisposable
    {
        AccountsDB _db;
        
        //TODO: add logger and resiliense lib
        public Repository(AccountsDB db)
        {
            _db = db;
        }

        public async Task<int?> GetBalance(string id)
        {
            return await _db.Transactions.Where(a => a.AccountId == id).SumAsync(t => t.Balance);
        }

        public async Task PostAmount(Guid id, TransactionModel transaction)
        {
            await _db.Transactions.InsertAsync(() => new Transaction { TransactionId = id.ToString(), AccountId = transaction.Account_Id.ToString(), Balance = transaction.Amount });
        }

        public async Task<Transaction> GetTransaction(string id)
        {
            return await _db.Transactions.Where(a => a.TransactionId == id).FirstOrDefaultAsync();
        }

        public async Task<TransactionVolume> GetTransactionVolume()
        {
            //TODO: probably not the most effective way, need to test performance
            var list = await _db.Transactions.GroupBy(t => new { t.AccountId }, t => t.AccountId).GroupBy(tg => tg.Count()).OrderByDescending(tg => tg.Key).FirstOrDefaultAsync();
            if (list != null)
                return new TransactionVolume { MaxVolume = list.Key, Accounts = list.Select(t => t.Key.AccountId).ToList() };
            else
                return null;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
