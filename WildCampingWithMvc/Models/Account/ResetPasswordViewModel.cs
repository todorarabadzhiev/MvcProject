using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.App_GlobalResources;

namespace WildCampingWithMvc.Models.Account
{
    public class ResetPasswordViewModel
    {
        [EmailAddress]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrNameIsRequired")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextEmail")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrPasswordIsRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "WarnPasswordLength", MinimumLength = 6)]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextPassword")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "WarnPasswordsDoNotMatch")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}