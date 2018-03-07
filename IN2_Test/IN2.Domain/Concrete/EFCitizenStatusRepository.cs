using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Tatooine.Domain.Abstract;
using Tatooine.Domain.Common;
using Tatooine.Domain.Entities;

namespace Tatooine.Domain.Concrete
{
    public class EFCitizenStatusRepository : ICitizenStatusRepository
    {
        #region Fields

        private EFDbContext context = new EFDbContext();
        private string logPath = ConfigurationManager.AppSettings["logPath"];

        #endregion

        #region Public Methods

        public IEnumerable<CitizenStatus> CitizensStatus
        {
            get
            {
                try
                {
                    return context.CitizenStatus;
                }
                catch (Exception ex)
                {
                    Task.Factory.StartNew(() => OutputFileLog.Instance.SetMessageLogging(this.logPath, ex.Message));
                    throw ex;
                }
            }
        }

        #endregion
    }
}
