using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.App_GlobalResources;

namespace WildCampingWithMvc.Models.Account
{
    public class VerifyCodeViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrProviderIsRequired")]
        public string Provider { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrCodeIsRequired")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextCode")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(ResourceType = typeof(GlobalResources), Name = "TextRememberBrowser")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}