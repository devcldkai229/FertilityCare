﻿using FertilityCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IMonitorReminderRepository : IBaseRepository<MonitorReminder, long>
{
    Task<IEnumerable<MonitorReminder>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<MonitorReminder>> GetByPlanIdAsync(Guid planId);
    Task<IEnumerable<MonitorReminder>> GetBySenderIdAsync(Guid senderId);
    Task<IEnumerable<MonitorReminder>> GetDueRemindersAsync(DateTime currentTime);
}
