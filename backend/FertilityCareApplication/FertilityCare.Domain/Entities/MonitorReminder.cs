using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class MonitorReminder
{
    public long Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid? ServicePackagePlanId { get; set; }

    public Guid? SenderId { get; set; }

    public MonitorReminderType ReminderType { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime ReminderDate { get; set; }

    public RecurrencePatternType? RecurrencePattern { get; set; }

    public bool IsComplete { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

}
