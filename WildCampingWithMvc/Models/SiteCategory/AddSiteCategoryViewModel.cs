using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WildCampingWithMvc.App_GlobalResources;

namespace WildCampingWithMvc.Models.SiteCategory
{
    public class AddSiteCategoryViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrNameIsRequired")]
        [MinLength(2, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrNameLength_2")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextSiteCategoryName")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(GlobalResources), Name = "TextDescription")]
        [AllowHtml]
        public string Description { get; set; }

        public string ImageFileName { get; set; }

        public string ImageFileData { get; set; }
    }
}