﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Events
{
    public interface IEventHandler<T> where T: IEvent
    {
        Task HandleAsync(T @event);

    }
}
