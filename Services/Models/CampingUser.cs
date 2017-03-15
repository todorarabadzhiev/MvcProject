using System;
using System.Collections.Generic;

namespace Services.Models
{
    public class CampingUser : ICampingUser
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime RegisteredOn { get; set; }
        public IEnumerable<CampingPlace> FavoriteCampingPlaces { get; set; }
        public IEnumerable<CampingPlace> MyCampingPlaces { get; set; }
    }
}
