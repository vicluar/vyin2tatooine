using System.Collections.Generic;

namespace Tatooine.Domain.Entities
{
    public class CitizenStatus
    {
        #region Properties

        public int ID { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Citizen> Citizens { get; set; }

        #endregion
    }
}
