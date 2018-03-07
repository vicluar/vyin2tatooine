using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Tatooine.Service.Model;

namespace Tatooine.Service
{
    [ServiceContract]
    public interface IUniverseService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool RegisterRebeldIdentification(List<RebeldPlanet> rebeldList);
    }
}
