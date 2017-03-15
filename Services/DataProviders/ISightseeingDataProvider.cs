﻿using Services.Models;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public interface ISightseeingDataProvider
    {
        IEnumerable<ISightseeing> GetAllSightseeings();
    }
}
