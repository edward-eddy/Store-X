using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Domain.Exceptions
{
    public class ProductNotFoundException(int id) : NotFoundException($"Product With Id: {id} Was Not Found!")
    {
    }
}
