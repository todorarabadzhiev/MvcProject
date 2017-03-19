using System.ComponentModel.DataAnnotations;
using CommonUtilities.Utilities;

namespace WildCampingWithMvc.Models.Manage
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = Messages.WarnPasswordLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = Messages.TextNewPassword)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Messages.TextConfirmNewPassword)]
        [Compare("NewPassword", ErrorMessage = Messages.WarnPasswordsDoNotMatch)]
        public string ConfirmPassword { get; set; }
    }
}