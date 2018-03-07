using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Tatooine.Domain.Abstract;
using Tatooine.Domain.Common;
using Tatooine.Domain.Entities;

namespace Tatooine.Domain.Concrete
{
    public class EFRoleRepository : IRoleRepository
    {
        #region Fields

        private EFDbContext context = new EFDbContext();
        private string logPath = ConfigurationManager.AppSettings["logPath"];

        #endregion

        #region Public Methods

        public IEnumerable<Role> Roles
        {
            get
            {
                try
                {
                    return context.Roles;
                }
                catch (Exception ex)
                {
                    Task.Factory.StartNew(() => OutputFileLog.Instance.SetMessageLogging(this.logPath, ex.Message));
                    throw ex;
                }
            }
        }

        public void SaveRole(Role role)
        {
            try
            {
                // New roles have ID == 0
                if (role.ID == 0)
                {
                    context.Roles.Add(role);
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() => OutputFileLog.Instance.SetMessageLogging(this.logPath, ex.Message));
                throw ex;
            }
        }

        #endregion
    }
}
