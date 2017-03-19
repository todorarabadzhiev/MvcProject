using System.ComponentModel.DataAnnotations;
using CommonUtilities.Utilities;

namespace WildCampingWithMvc.Models.Manage
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = Messages.TextPhoneNumber)]
        public string Number { get; set; }
    }
}