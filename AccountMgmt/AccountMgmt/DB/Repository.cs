using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmt.DB
{
    public static class Repository
    {
        public async static Task<int?> GetBalance(string id)
        {
            using (var db = new AccountsDB("Default"))
            {
                return await db.Accounts.Select(a => a.Balance).FirstOrDefaultAsync();
            }
        }
    }
}
