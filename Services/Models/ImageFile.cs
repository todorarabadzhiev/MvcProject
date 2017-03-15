using System;

namespace Services.Models
{
    public class ImageFile : IImageFile
    {
        public Guid Id { get; }

        public Guid CampingPlaceId { get; set; }

        public byte[] Data { get; set; }

        public string FileName { get; set; }
    }
}
