using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class Blog
{
    public long Id { get; set; } 
    public Guid UserProfileId { get; set; }

    public string Title { get; set; }
    public string Summary { get; set; }
    public string Content { get; set; }

    public string FeaturedImageUrl { get; set; }
    public string MetaKeywords { get; set; }
    public string MetaDescription { get; set; }

    public int Status { get; set; } = 2; 

    public virtual UserProfile UserProfile { get; set; }
}
