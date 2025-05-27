using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class Blog
{
    public long Id { get; set; }

    public Guid UserProfileId { get; set; }

    public virtual UserProfile UserProfile { get; set; }

    public string Title { get; set; }

    public string? Summary { get; set; }

    public string Content { get; set; }

    public string? FeaturedImageUrl { get; set; }

    public string? MetaKeywords { get; set; }

    public string? MetaDescription { get; set; }

    public BlogStatus? Status { get; set; }

    public int? ViewCount { get; set; }

}
