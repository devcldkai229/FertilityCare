using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.Exceptions;

public class NotFoundException : BaseException
{
    public override int StatusCode => 404;

    public NotFoundException(string message) : base(message)
    {
    }


}
