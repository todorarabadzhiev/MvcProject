using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.Utilities;

namespace WildCampingWithMvc.Models.Account
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = Messages.TextCode)]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = Messages.TextRememberBrowser)]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}