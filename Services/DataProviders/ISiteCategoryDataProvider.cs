using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public interface ISiteCategoryDataProvider
    {
        IEnumerable<ISiteCategory> GetAllSiteCategories();// Not deleted only
        IEnumerable<ISiteCategory> GetDeletedSiteCategories();
        ISiteCategory GetSiteCategoryById(Guid id);// Not deleted only
        void AddSiteCategory(string name, string description, byte[] imageFileData);
        void UpdateSiteCategory(Guid id, string name, string description, byte[] imageFileData);
        void DeleteSiteCategory(Guid id);
        void RecoverDeletedCategoryById(Guid id);
    }
}
