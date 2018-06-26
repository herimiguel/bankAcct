using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccts.Models{
    public class User : BaseEntity{
        public int Id {get; set; }
        public string FirstName {get; set; }
        public string LastName {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public double Balance {get; set;}
    }
}