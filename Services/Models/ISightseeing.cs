using System;

namespace Services.Models
{
    public interface ISightseeing
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsDeleted { get; set; }
        byte[] Image { get; set; }
    }
}
