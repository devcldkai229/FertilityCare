using FertilityCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Entities;

public class AppointmentReminder
{
    public long Id { get; set; }

    public Guid AppointmentId { get; set; }

    public Guid PatientId { get; set; }

    public DateTime? ReminderDate { get; set; }

    public string? ReminderMethod { get; set; }

    public bool IsSent { get; set; }

    public DateTime SentAt { get; set; }

    public AppointmentReminderStatus? Status { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

}
