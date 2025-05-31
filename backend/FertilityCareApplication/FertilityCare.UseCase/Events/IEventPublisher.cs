using FertilityCare.Domain.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event) where T: IEvent;
    }
}
