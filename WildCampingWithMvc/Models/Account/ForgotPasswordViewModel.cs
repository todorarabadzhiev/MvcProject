using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.App_GlobalResources;

namespace WildCampingWithMvc.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrNameIsRequired")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextEmail")]
        public string Email { get; set; }
    }
}