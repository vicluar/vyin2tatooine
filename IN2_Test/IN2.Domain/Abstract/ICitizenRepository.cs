using System.Collections.Generic;
using Tatooine.Domain.Entities;

namespace Tatooine.Domain.Abstract
{
    public interface ICitizenRepository
    {
        IEnumerable<Citizen> Citizens { get; }

        void SaveCitizen(Citizen citizen);

        Citizen GetCitizen(int citizenID);
    }
}
