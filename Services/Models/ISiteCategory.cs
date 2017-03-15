using System;

namespace Services.Models
{
    public interface ISiteCategory
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
