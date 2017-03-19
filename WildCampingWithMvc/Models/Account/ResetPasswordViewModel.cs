using System.ComponentModel.DataAnnotations;
using CommonUtilities.Utilities;

namespace WildCampingWithMvc.Models.Account
{
    public class ResetPasswordViewModel
    {
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

        public string Code { get; set; }
    }
}