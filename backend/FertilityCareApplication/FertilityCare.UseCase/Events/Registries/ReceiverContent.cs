using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Events.Registries
{
    public class ReceiverContent
    {
        public string To;
        
        public string Subject;
        
        public string Body;

        public bool IsHtml = true;
    }
}
