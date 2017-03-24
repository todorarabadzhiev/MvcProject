using System;

namespace DbModelsContracts
{
    public interface IDbSightseeingType
    {
        Guid Id { get; }

        string Name { get; set; }
    }
}