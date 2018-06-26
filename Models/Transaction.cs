using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccts.Models{
    public class Transaction : BaseEntity{
        public long Id {get; set; }
        public float Amount {get; set;}
        public DateTime XDate {get; set;}
        public int users_id {get; set;}
    }
}