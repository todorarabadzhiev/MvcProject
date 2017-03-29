using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.App_GlobalResources;

namespace WildCampingWithMvc.Models.Manage
{
    public class AddPhoneNumberViewModel
    {
        [Phone]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrPhoneIsRequired")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextPhoneNumber")]
        public string Number { get; set; }
    }
}