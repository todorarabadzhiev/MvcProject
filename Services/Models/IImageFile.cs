using System;

namespace Services.Models
{
    public interface IImageFile
    {
        Guid Id { get; }

        byte[] Data { get; set; }

        string FileName { get; set; }
        Guid CampingPlaceId { get; set; }
    }
}
