using Services.Models;
using System.Collections.Generic;

namespace WildCampingWithMvc.Models.Sightseeing
{
    public class SightseeingsViewModel
    {
        public IEnumerable<ISightseeing> Sightseeings { get; set; }
    }
}