using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.Utilities;

namespace WildCampingWithMvc.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = Messages.TextEmail)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = Messages.TextPassword)]
        public string Password { get; set; }

        [Display(Name = Messages.TextRememberMe)]
        public bool RememberMe { get; set; }
    }
}