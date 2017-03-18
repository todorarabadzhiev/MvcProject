using Services.Models;
using System.Collections.Generic;

namespace WildCampingWithMvc.Models.Home
{
    public class IndexViewModel
    {
        public IEnumerable<ICampingPlace> LastCampingPlaces { get; set; }
    }
}