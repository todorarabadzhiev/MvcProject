using System.ComponentModel.DataAnnotations;
using CommonUtilities.Utilities;

namespace WildCampingWithMvc.Models.Manage
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = Messages.TextCode)]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = Messages.TextPhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}