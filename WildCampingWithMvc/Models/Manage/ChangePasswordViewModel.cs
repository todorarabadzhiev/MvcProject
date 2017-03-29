using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.App_GlobalResources;

namespace WildCampingWithMvc.Models.Manage
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrPasswordIsRequired")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextCurrentPassword")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrPasswordIsRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "WarnPasswordLength", MinimumLength = 6)]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextNewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextConfirmNewPassword")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "WarnPasswordsDoNotMatch")]
        public string ConfirmPassword { get; set; }
    }
}