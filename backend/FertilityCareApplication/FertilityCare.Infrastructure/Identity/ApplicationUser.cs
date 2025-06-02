using FertilityCare.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual UserProfile? UserProfile { get; set; }
    }
}
