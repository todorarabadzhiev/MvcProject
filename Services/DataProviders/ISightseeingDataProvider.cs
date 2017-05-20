using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public interface ISightseeingDataProvider
    {
        IEnumerable<ISightseeing> GetAllSightseeings();// Not deleted only
        IEnumerable<ISightseeing> GetDeletedSightseeings();
        ISightseeing GetSightseeingById(Guid id);// Not deleted only
        void AddSightseeing(string name, string description, byte[] imageFileData);
        void UpdateSightseeing(Guid id, string name, string description, byte[] imageFileData);
        void DeleteSightseeing(Guid id);
        void RecoverDeletedSightseeingById(Guid id);
    }
}
