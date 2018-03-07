using System.Runtime.Serialization;

namespace Tatooine.Service.Model
{
    [DataContract]
    public class RebeldPlanet
    {
        #region Members

        [DataMember]
        public string RebeldName { get; set; }

        [DataMember]
        public string Planet { get; set; }

        #endregion
    }
}