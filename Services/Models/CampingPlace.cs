using System;
using System.Collections.Generic;

namespace Services.Models
{
    public class CampingPlace : ICampingPlace
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AddedBy { get; set; }
        public string Description { get; set; }
        public string GoogleMapsUrl { get; set; }
        public bool HasWater { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AddedOn { get; set; }
        public IEnumerable<Guid> SightseeingIds { get; set; }
        public IEnumerable<Guid> SiteCategoriesIds { get; set; }
        public IEnumerable<string> SiteCategoriesNames { get; set; }
        public IEnumerable<string> SightseeingNames { get; set; }
        public IList<IImageFile> ImageFiles { get; set; }
    }
}
