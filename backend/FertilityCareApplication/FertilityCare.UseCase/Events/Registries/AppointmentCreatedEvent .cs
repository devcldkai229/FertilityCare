using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Events.Registries
{
    public class AppointmentCreatedEvent : IEvent
    {
        
        public Appointment Appointment { get; set; }

        public AppointmentCreatedEvent(Appointment appointment)
        {
            this.Appointment = appointment;
        }

    }
}
