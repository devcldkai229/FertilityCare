using FertilityCare.UseCase.Events.Registries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(ReceiverContent content);

    }
}
