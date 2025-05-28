using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IMediaFilesRepository : IBaseRepository<MediaFile, Guid>    
{
    Task<IEnumerable<MediaFile>> GetByOwnerIdAsync(Guid ownerId);
    Task<IEnumerable<MediaFile>> GetByFileTypeAsync(string fileType);
    Task<IEnumerable<MediaFile>> GetPublicFilesAsync();
}
