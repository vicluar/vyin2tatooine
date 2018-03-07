
namespace Tatooine.Domain.Entities
{
    public class Citizen
    {
        #region Properties

        public int ID { get; set; }
        public string Name { get; set; }
        public string SpecieType { get; set; }
        public int RoleID { get; set; }
        public int CitizenStatusID { get; set; }

        public virtual Role Role { get; set; }
        public virtual CitizenStatus CitizenStatus { get; set; }

        #endregion
    }
}
