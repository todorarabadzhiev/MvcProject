using System;
using WildCampingWithMvc.Models.CampingPlace;

namespace WildCampingWithMvc.Models.Sightseeing
{
    public class SightseeingDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageData { get; set; }
        public MultipleCampingPlacesViewModel Places { get; set; }
    }
}