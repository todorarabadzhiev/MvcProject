using CommonUtilities.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WildCampingWithMvc.App_GlobalResources;

namespace WildCampingWithMvc.Models.CampingPlace
{
    public class AddCampingPlaceViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrNameIsRequired")]
        [MinLength(2, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrNameLength_2")]
        [Display(ResourceType = typeof(GlobalResources), Name = "TextPlaceName")]
        public string Name { get; set; }
        //public bool IsAuthor { get; set; }

        [Display(ResourceType = typeof(GlobalResources), Name = "TextDescription")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(ResourceType = typeof(GlobalResources), Name = "TextGoogleMapsUrl")]
        public string GoogleMapsUrl { get; set; }

        [Display(ResourceType = typeof(GlobalResources), Name = "TextHasWater")]
        public bool HasWater { get; set; }
        public IList<string> SightseeingNames { get; set; }
        public IList<string> SiteCategoriesNames { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "ErrAtLeasOneImageRequired")]
        public IList<string> ImageFileNames { get; set; }

        public IList<string> ImageFilesData { get; set; }
    }
}