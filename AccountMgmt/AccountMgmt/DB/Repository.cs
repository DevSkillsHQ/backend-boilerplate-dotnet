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

        public async Task<int?> GetBalance(string id)
        {
            return await _db.Transactions.Where(a => a.AccountId == id).SumAsync(t => t.Balance);
        }

        public async Task PostAmount(string id, int amount)
        {
            await _db.Transactions.InsertAsync(() => new Transaction { TransactionId = Guid.NewGuid().ToString(), AccountId = id, Balance = amount });
        }

        public async Task<Transaction> GetTransaction(string id)
        {
            return await _db.Transactions.Where(a => a.AccountId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetTransactionVolume()
        {
            var list = await _db.Transactions.GroupBy(t => t.AccountId).GroupBy(tl => tl.Count()).OrderBy(t => t.Key).FirstOrDefaultAsync();
            if (list != null)
                return list.Accts.Select(t => t.Key);
            else
                return null;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
