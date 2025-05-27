using FertilityCare.Domain.Interfaces.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.UnitOfWorks;

public interface IServicePackageUnitOfWork
{
    IServicePackagePlanRepository _servicePackagePlanRepository { get; }

    IServicePackagePlanStepRepository _servicePackagePlanStepRepository { get; }

    IServicePackagePlanExtensionRepository _servicePackagePlanExtensionRepository { get; }

    IMonitorReminderRepository monitorReminderRepository { get; }
}
