using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Domain.Exceptions
{
    public class BasketNotFoundException(string id) : NotFoundException($"Baskit With Id {id} Was Not Found!")
    {
    }
}
