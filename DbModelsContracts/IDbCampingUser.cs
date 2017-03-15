using System;
using System.Collections.Generic;

namespace DbModelsContracts
{
    public interface IDbCampingUser
    {
        Guid Id { get; }
        Guid RegisteredUserId { get; set; }//take from Account

        string Name { get; set; }
        DateTime RegisteredOn { get; set; }

        ICollection<IDbCampingPlace> MyCampingPlaces { get; set; }

        ICollection<IDbCampingPlace> FavoriteCampingPlaces { get; set; }
    }
}