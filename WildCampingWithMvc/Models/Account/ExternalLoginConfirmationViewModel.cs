using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.Utilities;

namespace WildCampingWithMvc.Models.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = Messages.TextEmail)]
        public string Email { get; set; }
    }
}