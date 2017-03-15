using System;

namespace Services.Models
{
    public class SiteCategory : ISiteCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
