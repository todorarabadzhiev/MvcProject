using System;
using System.Collections.Generic;

namespace DbModelsContracts
{
    public interface IDbSiteCategory
    {
        Guid Id { get; set; }

        string Name { get; set; }
        ICollection<IDbCampingPlace> CampingPlaces { get; set; }
    }
}