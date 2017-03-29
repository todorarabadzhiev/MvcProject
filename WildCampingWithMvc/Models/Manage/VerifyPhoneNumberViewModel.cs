using System.ComponentModel.DataAnnotations;
using WildCampingWithMvc.App_GlobalResources;

namespace WildCampingWithMvc.Models.Manage
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrCodeIsRequired")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextCode")]
        public string Code { get; set; }

        [Phone]
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrPhoneIsRequired")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextPhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}