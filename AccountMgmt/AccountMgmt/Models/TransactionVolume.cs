using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmt.Models
{
    public class TransactionVolume
    {
        public int MaxVolume { get; set; }
        public List<string> Accounts { get; set; }
    }
}
