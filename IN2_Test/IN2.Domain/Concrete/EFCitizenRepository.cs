using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tatooine.Domain.Abstract;
using Tatooine.Domain.Common;
using Tatooine.Domain.Entities;

namespace Tatooine.Domain.Concrete
{
    public class EFCitizenRepository : ICitizenRepository
    {
        #region Fields

        private EFDbContext context = new EFDbContext();
        private string logPath = ConfigurationManager.AppSettings["logPath"];

        #endregion

        #region Public Methods

        public IEnumerable<Citizen> Citizens
        {
            get
            {
                try
                {
                    // Include Role and CitizenStatus information
                    return context.Citizens
                        .Include(r => r.Role)
                        .Include(cs => cs.CitizenStatus);
                }
                catch (Exception ex)
                {
                    Task.Factory.StartNew(() => OutputFileLog.Instance.SetMessageLogging(this.logPath, ex.Message));
                    throw ex;
                }
            }
        }

        public void SaveCitizen(Citizen citizen)
        {
            try
            {
                // New citizens have ID == 0
                if (citizen.ID == 0)
                {
                    context.Citizens.Add(citizen);
                }
                else
                {
                    // Verify the citizen entity and then update
                    Citizen dbEntry = context.Citizens.Find(citizen.ID);

                    if (dbEntry != null)
                    {
                        dbEntry.Name = citizen.Name;
                        dbEntry.SpecieType = citizen.SpecieType;
                        dbEntry.RoleID = citizen.RoleID;
                        dbEntry.CitizenStatusID = citizen.CitizenStatusID;
                    }
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() => OutputFileLog.Instance.SetMessageLogging(this.logPath, ex.Message));
                throw ex;
            }
        }

        public Citizen GetCitizen(int citizenID)
        {
            try
            {
                // Include Role and CitizenStatus information
                return context.Citizens
                    .Where(c => c.ID == citizenID)
                    .Include(r => r.Role)
                    .Include(cs => cs.CitizenStatus)
                    .FirstOrDefault();
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
