using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Domain.Exceptions.BadRequest
{
    public class OrderCreateBadRequestException() : BadRequestException("Invalid Operation When Create Order")
    {
    }
}
