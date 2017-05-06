using Services.Models;
using System.Collections.Generic;

namespace WildCampingWithMvc.Models.SiteCategory
{
    public class SiteCategoriesViewModel
    {
        public IEnumerable<ISiteCategory> SiteCategories { get; set; }
    }
}