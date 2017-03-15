using Services.Models;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public interface ISiteCategoryDataProvider
    {
        IEnumerable<ISiteCategory> GetAllSiteCategories();
    }
}
