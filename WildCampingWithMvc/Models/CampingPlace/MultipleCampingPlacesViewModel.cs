using Services.Models;
using System.Collections.Generic;

namespace WildCampingWithMvc.Models.CampingPlace
{
    public class MultipleCampingPlacesViewModel
    {
        public IEnumerable<ICampingPlace> CampingPlaces { get; set; }
    }
}