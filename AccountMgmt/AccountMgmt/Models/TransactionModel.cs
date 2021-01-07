using AccountMgmt.DB;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmt.Models
{
    public class TransactionModel
    {
        [Required]
        public Guid? Account_Id { get; set; }
        [Required]
        public int? Amount { get; set; }

    }
}
