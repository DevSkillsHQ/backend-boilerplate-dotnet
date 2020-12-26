using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmt.Models
{
    public class TransactionModel
    {
        public Guid Account_Id { get; set; }
        public int Amount { get; set; }
    }
}
