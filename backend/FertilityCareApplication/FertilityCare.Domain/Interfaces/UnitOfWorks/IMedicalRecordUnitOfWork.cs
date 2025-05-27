using FertilityCare.Domain.Interfaces.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.UnitOfWorks;

public interface IMedicalRecordUnitOfWork
{
    ITestResultRepository _testResultRepository { get; }

    IPrescriptionRepository _prescriptionRepository { get; }

    IPrescriptionItemRepository _prescriptionItemRepository { get; }

}
