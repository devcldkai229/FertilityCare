using FertilityCare.Domain.Interfaces.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Events
{
    public class EventPublisher : IEventPublisher
    {

        private readonly IServiceProvider _serviceProvider;

        public EventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishAsync<T>(T @event) where T : IEvent
        {
            try
            {
                var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
                var handlers = _serviceProvider.GetServices(handlerType);

                var tasks = new List<Task>();
                foreach (var handler in handlers)
                {
                    var handleMethod = handlerType.GetMethod("HandleAsync");
                    if (handleMethod != null)
                    {
                        var task = (Task)handleMethod.Invoke(handler, new object[] { @event });
                        tasks.Add(task);
                    }
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
