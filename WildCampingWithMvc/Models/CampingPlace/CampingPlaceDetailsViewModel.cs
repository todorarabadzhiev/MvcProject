using Services.Models;
using System;
using System.Collections.Generic;

namespace WildCampingWithMvc.Models.CampingPlace
{
    public class CampingPlaceDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public string Description { get; set; }

        public string GoogleMapsUrl { get; set; }

        public bool HasWater { get; set; }
        public IEnumerable<ISightseeing> Sightseeings { get; set; }
        public IEnumerable<ISiteCategory> SiteCategories { get; set; }

        public IList<string> ImageFileNames { get; set; }

        public IList<byte[]> ImageFilesData { get; set; }
    }
}