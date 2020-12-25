using LinqToDB.Configuration;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmt.DB
{
    public partial class AccountsDB: DataConnection
    {
        public AccountsDB(LinqToDbConnectionOptions<AccountsDB> options) : base(options)
        {

        }
    }
}
