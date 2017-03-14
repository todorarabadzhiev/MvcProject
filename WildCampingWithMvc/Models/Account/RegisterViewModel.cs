using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.Utilities;

namespace WildCampingWithMvc.Models.Account
{
    public class RegisterViewModel
    {
        // CUSTOM CODE STARTS!!!!!!!!!!
        [Required]
        [Display(Name = Messages.TextFirstName)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = Messages.TextLastName)]
        public string LastName { get; set; }
        // CUSTOM CODE ENDS!!!!!!!!!!!!


        [Required]
        [EmailAddress]
        [Display(Name = Messages.TextEmail)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = Messages.WarnPasswordLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = Messages.TextPassword)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Messages.TextConfirmPassword)]
        [Compare("Password", ErrorMessage = Messages.WarnPasswordsDoNotMatch)]
        public string ConfirmPassword { get; set; }
    }
}