using System;

namespace Services.Models
{
    public class Sightseeing : ISightseeing
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] Image { get; set; }
    }
}
