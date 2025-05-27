using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class MediaFile
{
    public Guid Id { get; set; }

    public string? PublicId { get; set; }

    public string? Url { get; set; }

    public string? SecureUrl { get; set; }

    public string? Folder { get; set; }

    public string? FileName { get; set; }

    public string? FileType { get; set; }

    public string? MimeType { get; set; }

    public string? ResourceType { get; set; }

    public string? Format { get; set; }

    public long? Size { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public decimal? Duration { get; set; }

    public string? Tags { get; set; }

    public string? Context { get; set; }

    public string? Transformation { get; set; }

    public Guid? OwnerId { get; set; }

    public string? OwnerType { get; set; }

    public string RelatedEntityId { get; set; } = "#NoData";

    public string RelatedEntityType { get; set; } = "#NoData";

    public bool IsPublic { get; set; } = false;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
