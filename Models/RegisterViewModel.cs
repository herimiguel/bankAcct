using System.ComponentModel.DataAnnotations;

namespace BankAccts.Models{
    public class RegisterViewModel : BaseEntity{
        [Required]
        [MinLength(2)]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name: ")]
        public string LastName {get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address: ")]
        public string Email {get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Display(Name =" Password: ")]
        public string Password {get; set;}

        [Display(Name = "Confirm Password: ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password must Match!")]
        public string PassConf {get; set;}
        

    }
}