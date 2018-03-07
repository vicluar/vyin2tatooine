using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Tatooine.Domain.Common;
using Tatooine.Service.Model;

namespace Tatooine.Service
{
    public class UniverseService : IUniverseService
    {
        #region Exposed Methods

        public bool RegisterRebeldIdentification(List<RebeldPlanet> rebeldList)
        {
            try
            {
                Parallel.ForEach(rebeldList, rebel =>
                {
                    Task.Factory.StartNew(() => 
                        OutputFileLog.Instance.SetMessageToFile(
                            ConfigurationManager.AppSettings["rebeldFileLogPath"],
                            ConfigurationManager.AppSettings["rebeldFileLogName"],
                            string.Format("rebeld {0} on {1} at {2} {3}", rebel.RebeldName, rebel.Planet, DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString())));
                });

                return true;
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() => OutputFileLog.Instance.SetMessageLogging(ConfigurationManager.AppSettings["logPath"], ex.Message));

                return false;
            }
        }

        #endregion
    }
}
