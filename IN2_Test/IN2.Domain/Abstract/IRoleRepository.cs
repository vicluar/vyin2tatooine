using System.Collections.Generic;
using Tatooine.Domain.Entities;

namespace Tatooine.Domain.Abstract
{
    public interface IRoleRepository
    {
        IEnumerable<Role> Roles { get; }

        void SaveRole(Role role);
    }
}
