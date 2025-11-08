using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Domain.Exceptions.NotFound
{
    public abstract class NotFoundException(string message) : Exception(message)
    {
    }
}
