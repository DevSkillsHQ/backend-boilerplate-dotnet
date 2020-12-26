using AccountMgmt.Models;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmt.DB
{
    public class Repository: IDisposable
    {
        AccountsDB _db;
        
        public Repository(AccountsDB db)
        {
            _db = db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<int> GetBalance(string id)
        {
            return await _db.Transactions.Where(a => a.AccountId == id).SumAsync(a => a.Balance);
        }

        public async Task PostAmount(string id, int amount)
        {
            await _db.Transactions.InsertAsync(() => new Transaction { AccountId = id, Balance = amount });
        }

        public async Task<TransactionModel> GetTransaction(string id)
        {
            return await _db.Transactions.Where(a => a.AccountId == id)
                .Select(a => new TransactionModel { Account_Id = new Guid(a.AccountId), Amount = a.Balance }).FirstOrDefaultAsync();
        }
    }
}
