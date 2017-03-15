using System;
using System.Collections.Generic;

namespace DbModelsContracts
{
    public interface IDbCampingPlace
    {
        Guid Id { get; }
        string Name { get; set; }
        string Description { get; set; }
        string ClosestTown { get; set; }
        string GoogleMapsUrl { get; set; }
        bool WaterOnSite { get; set; }
        DateTime AddedOn { get; set; }
        IDbCampingUser AddedBy { get; set; }

        ICollection<IDbSiteCategory> SiteCategories { get; set; }

        ICollection<IDbSightseeing> Sightseeings { get; set; }
    }
}
