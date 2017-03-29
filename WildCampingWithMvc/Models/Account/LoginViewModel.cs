using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.App_GlobalResources;

namespace WildCampingWithMvc.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrNameIsRequired")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextEmail")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrPasswordIsRequired")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextPassword")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(GlobalResources), Name = "TextRememberMe")]
        public bool RememberMe { get; set; }
    }
}