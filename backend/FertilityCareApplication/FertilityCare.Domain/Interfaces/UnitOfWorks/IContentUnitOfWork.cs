using FertilityCare.Domain.Interfaces.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.UnitOfWorks;

public interface IContentUnitOfWork
{
    IFeedbackRepository _feedbackRepository { get; }

    IBlogRepository _blogRepository { get; }

    IMediaFilesRepository _mediaFilesRepository { get; }

}
