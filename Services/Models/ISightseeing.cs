using System;

namespace Services.Models
{
    public interface ISightseeing
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Type { get; set; }
    }
}
