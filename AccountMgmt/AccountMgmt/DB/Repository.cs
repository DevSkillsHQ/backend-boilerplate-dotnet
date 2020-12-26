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

        public async Task<int?> GetBalance(string id)
        {
            return await _db.Accounts.Select(a => a.Balance).FirstOrDefaultAsync();
        }

        public async Task PostAmount(string id, int amount)
        {
            await _db.BeginTransactionAsync();
            try
            {
                await _db.Accounts.InsertOrUpdateAsync(() => new Account { AccountId = id, Balance = amount }, a => new Account { AccountId = id, Balance = a.Balance + amount });
            }
            catch
            {
                await _db.RollbackTransactionAsync();
                return;
            }
            await _db.CommitTransactionAsync();
        }
    }
}
