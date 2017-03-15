using System;
using System.Collections.Generic;

namespace Services.Models
{
    public interface ICampingUser
    {
        Guid Id { get; set; }
        string ApplicationUserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string UserName { get; set; }
        DateTime RegisteredOn { get; set; }
        IEnumerable<CampingPlace> MyCampingPlaces { get; set; }
        IEnumerable<CampingPlace> FavoriteCampingPlaces { get; set; }
    }
}
