using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public interface ISightseeingDataProvider
    {
        IEnumerable<ISightseeing> GetAllSightseeings();
        ISightseeing GetSightseeingById(Guid id);
    }
}
