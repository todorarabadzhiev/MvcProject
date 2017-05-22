using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public interface ICampingPlaceDataProvider
    {
        IEnumerable<ICampingPlace> GetUserCampingPlaces(string userName);// Not deleted only
        IEnumerable<ICampingPlace> GetSiteCategoryCampingPlaces(string categoryName);// Not deleted only
        IEnumerable<ICampingPlace> GetSightseeingCampingPlaces(string sightseeingName);// Not deleted only
        IEnumerable<ICampingPlace> GetAllCampingPlaces();// Not deleted only
        IEnumerable<ICampingPlace> GetDeletedCampingPlaces();
        IEnumerable<ICampingPlace> GetCampingPlacesBySearchName(string searchedName);// Not deleted only
        IEnumerable<ICampingPlace> GetLatestCampingPlaces(int count);// Not deleted only
        IEnumerable<ICampingPlace> GetCampingPlaceById(Guid id);// Not deleted only
        void AddCampingPlace(string name, string addedBy, string description, string googleMapsUrl,
                bool hasWater, IEnumerable<string> sightseeingNames, IEnumerable<string> siteCategoryNames,
                IList<string> imageFileNames, IList<byte[]> imageFiles);
        void UpdateCampingPlace(Guid id, string name, string description, string googleMapsUrl,
                bool hasWater, IEnumerable<string> sightseeingNames, IEnumerable<string> siteCategoryNames,
                IList<string> imageFileNames, IList<byte[]> imageFiles);
        void DeleteCampingPlace(Guid id);
        void RecoverDeletedCampingPlaceById(Guid id);
    }
}
