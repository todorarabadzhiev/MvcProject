using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public interface ISiteCategoryDataProvider
    {
        IEnumerable<ISiteCategory> GetAllSiteCategories();
        ISiteCategory GetSiteCategoryById(Guid id);
        void AddSiteCategory(string name, string description, byte[] imageFileData);
    }
}
