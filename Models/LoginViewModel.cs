using System.ComponentModel.DataAnnotations;

namespace BankAccts.Models{
    public class LoginViewModel : BaseEntity{
        [Display(Name= "Email: ")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        public string Password {get; set;}
    }
}