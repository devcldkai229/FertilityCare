using FertilityCare.Domain.Interfaces.Events;
using FertilityCare.UseCase.Events.Registries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Events.Handlers
{
    public class AppointmentEmailEventHandler : IEventHandler<AppointmentCreatedEvent>
    {
        public Task HandleAsync(AppointmentCreatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
