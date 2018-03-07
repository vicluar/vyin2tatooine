using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Tatooine.Domain.Abstract;
using Tatooine.Domain.Common;
using Tatooine.Domain.Entities;

namespace Tatooine.WebUI.Pages
{
    public partial class Roles : System.Web.UI.Page
    {
        #region Properties

        [Inject]
        public ICitizenRepository citizenRepository { get; set; }

        [Inject]
        public IRoleRepository roleRepository { get; set; }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlMessage.Visible = false;

            try
            {
                if (!this.IsPostBack)
                {
                    this.FillUserRoleList();
                }
            }
            catch (Exception ex)
            {
                this.HandleError(ex, "There is an error loading Users in Role list.");
            }
        }

        #endregion

        #region Control Events

        protected void btnSaveRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsValid && this.IsValidForm())
                {
                    Role role = new Role 
                    { 
                        Description = txtDescription.Text.Trim()
                    };

                    roleRepository.SaveRole(role);

                    this.ShowSuccess("The Role has been saved successfully");
                    this.FillUserRoleList();
                    this.CleanControls();
                }
            }
            catch (HttpException httpEx)
            {
                this.HandleError(httpEx, "There is an error on form validation.");
            }
            catch (Exception ex)
            {
                this.HandleError(ex, "There is an error trying to save the role information.");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validate controls data.
        /// </summary>
        /// <returns></returns>
        private bool IsValidForm()
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()) || !Regex.IsMatch(txtDescription.Text.Trim(), ConfigurationManager.AppSettings["regexAlphaNumericSpace"]))
                return false;

            return true;
        }

        /// <summary>
        /// Bind citizen list to Grid View.
        /// </summary>
        private void FillUserRoleList()
        {
            gvRoles.DataSource = GetCitizenList();
            gvRoles.DataBind();
        }

        /// <summary>
        /// Get citizen list and set it to ASP.NET Cache
        /// </summary>
        /// <returns></returns>
        private List<Citizen> GetCitizenList()
        {
            List<Citizen> citizenList;

            if (Cache["CitizenList"] != null)
                citizenList = Cache["CitizenList"] as List<Citizen>;

            citizenList = citizenRepository.Citizens.ToList();

            Cache["CitizenList"] = citizenList;

            return citizenList;
        }

        private void CleanControls()
        {
            txtDescription.Text = string.Empty;
        }

        private void HandleError(Exception error, string userMessage)
        {
            Task.Factory.StartNew(() => OutputFileLog.Instance.SetMessageLogging(ConfigurationManager.AppSettings["logPath"], error.Message));

            imgMessage.ImageUrl = "~/Images/Error.gif";
            lblMessage.Text = userMessage;
            pnlMessage.Visible = true;
        }

        private void ShowSuccess(string userMessage)
        {
            imgMessage.ImageUrl = "~/Images/Success.gif";
            lblMessage.Text = userMessage;
            pnlMessage.Visible = true;
        }

        #endregion
    }
}