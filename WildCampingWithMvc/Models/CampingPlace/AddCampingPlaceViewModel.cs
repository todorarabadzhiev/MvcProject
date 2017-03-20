using CommonUtilities.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WildCampingWithMvc.Models.CampingPlace
{
    public class AddCampingPlaceViewModel
    {
        [Required(ErrorMessage = Messages.ErrNameIsRequired)]
        [MinLength(2, ErrorMessage = Messages.ErrNameLength_2)]
        [DisplayName(Messages.TextPlaceName)]
        public string Name { get; set; }

        [DisplayName(Messages.TextDescription)]
        public string Description { get; set; }

        [DisplayName(Messages.TextGoogleMapsUrl)]
        public string GoogleMapsUrl { get; set; }

        [DisplayName(Messages.TextHasWater)]
        public bool HasWater { get; set; }
        public IList<string> SightseeingNames { get; set; }
        public IList<string> SiteCategoryNames { get; set; }

        [Required(ErrorMessage = Messages.ErrAtLeasOneImageRequired)]
        public IList<string> ImageFileNames { get; set; }

        public IList<string> ImageFilesData { get; set; }
    }
}