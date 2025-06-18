using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Domain.Exceptions
{
    public class NotFoundException(string message = "Recurso não encontrado") : Exception(message) { }
}
