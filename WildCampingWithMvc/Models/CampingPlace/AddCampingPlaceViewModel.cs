using System.Collections.Generic;

namespace WildCampingWithMvc.Models.CampingPlace
{
    public class AddCampingPlaceViewModel
    {
        public string Name { get; set; }
        public string AddedBy { get; set; }
        public string Description { get; set; }
        public string GoogleMapsUrl { get; set; }
        public bool HasWater { get; set; }
        public IEnumerable<string> SightseeingNames { get; set; }
        public IEnumerable<string> SiteCategoryNames { get; set; }
        public IList<string> ImageFileNames { get; set; }
        public IList<byte[]> ImageFilesData { get; set; }
    }
}