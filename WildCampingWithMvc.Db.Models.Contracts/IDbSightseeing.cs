using System;
using System.Collections.Generic;

namespace DbModelsContracts
{
    public interface IDbSightseeing
    {
        Guid Id { get; }

        string Name { get; set; }
        IDbSightseeingType Type { get; set; }
        ICollection<IDbCampingPlace> CampingPlaces { get; set; }
    }
}