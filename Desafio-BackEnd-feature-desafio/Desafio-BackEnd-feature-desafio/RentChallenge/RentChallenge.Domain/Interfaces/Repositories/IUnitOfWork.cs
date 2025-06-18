using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
