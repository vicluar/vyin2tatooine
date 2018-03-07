using System.Collections.Generic;
using Tatooine.Domain.Entities;

namespace Tatooine.Domain.Abstract
{
    public interface ICitizenStatusRepository
    {
        IEnumerable<CitizenStatus> CitizensStatus { get; }
    }
}
