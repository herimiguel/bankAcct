using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BankAccts.Models{
    public class BankViewModel : BaseEntity 
    {
        [Display(Name= "Deposit/Withdraw")]
        [Required]
        public float Amount { get; set; }
    }
}