using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Tatooine.Domain.Abstract;
using Tatooine.Domain.Common;
using Tatooine.Domain.Entities;
using Tatooine.WebUI.Enums;

namespace Tatooine.WebUI.Pages
{
    public partial class Citizens : System.Web.UI.Page
    {
        #region Properties

        [Inject]
        public ICitizenRepository citizenRepository { get; set; }

        [Inject]
        public ICitizenStatusRepository citizenStatusRepository { get; set; }

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
                    this.InitializeControls();
                }

                // Citizen List always is going to be refresh.
                this.FillCitizensList();
            }
            catch (Exception ex)
            {
                this.HandleError(ex, "There is an error loading citizens list.");
            }
        }

        #endregion

        #region Control Events

        protected void btnSaveCitizen_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsValid && this.IsValidForm())
                {
                    Citizen citizen = new Citizen
                    {
                        ID = string.IsNullOrEmpty(hfCitizenID.Value.ToString()) ? 0 : int.Parse(hfCitizenID.Value.ToString()),
                        Name = txtName.Text.Trim(),
                        SpecieType = txtSpecieType.Text.Trim(),
                        RoleID = int.Parse(ddlRole.SelectedItem.Value),
                        CitizenStatusID = int.Parse(ddlCitizenStatus.SelectedItem.Value)
                    };

                    citizenRepository.SaveCitizen(citizen);

                    this.ShowSuccess("The citizen has been saved successfully");
                    this.FillCitizensList();
                    this.CleanControls();
                }
            }
            catch (HttpException httpEx)
            {
                // Exception for IsValid propertie
                this.HandleError(httpEx, "There is an error on form validation.");
            }
            catch (ArgumentOutOfRangeException argumentEx)
            {
                // Exception for any validation control
                this.HandleError(argumentEx, "There is an error on List Controls.");
            }
            catch (Exception ex)
            {
                // General Exception
                this.HandleError(ex, "There is an error trying to save the citizen information.");
            }
        }

        protected void gvCitizens_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "_userRole") // Assing User to Role
                    EnableDisableControls(CitizenOperation.UserToRole);
                else if (e.CommandName == "_updateUser") // Update User Information
                    EnableDisableControls(CitizenOperation.Update);

                this.ShowUserDetail(int.Parse(e.CommandArgument.ToString()));
            }
            catch (Exception ex)
            {
                this.HandleError(ex, "There is an error trying to get the citizen information.");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes web controls in the page.
        /// </summary>
        private void InitializeControls()
        {
            ddlCitizenStatus.DataSource = citizenStatusRepository.CitizensStatus.ToList();
            ddlCitizenStatus.DataValueField = "ID";
            ddlCitizenStatus.DataTextField = "Description";
            ddlCitizenStatus.DataBind();

            ddlRole.DataSource = roleRepository.Roles.ToList();
            ddlRole.DataValueField = "ID";
            ddlRole.DataTextField = "Description";
            ddlRole.DataBind();
        }

        /// <summary>
        /// Clean and Enable web controls
        /// </summary>
        private void CleanControls()
        {
            txtName.Text = string.Empty;
            txtName.Enabled = true;
            txtSpecieType.Text = string.Empty;
            txtSpecieType.Enabled = true;
            ddlRole.SelectedIndex = 0;
            ddlRole.Enabled = true;
            ddlCitizenStatus.SelectedIndex = 0;
            ddlCitizenStatus.Enabled = true;
            hfCitizenID.Value = string.Empty;
        }

        /// <summary>
        /// Show User Detail information in the web Controls.
        /// </summary>
        /// <param name="citizenID"></param>
        private void ShowUserDetail(int citizenID)
        {
            Citizen citizen = citizenRepository.GetCitizen(citizenID);

            hfCitizenID.Value = citizen.ID.ToString();
            txtName.Text = citizen.Name;
            txtSpecieType.Text = citizen.SpecieType;
            ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByValue(citizen.Role.ID.ToString()));
            ddlCitizenStatus.SelectedIndex = ddlCitizenStatus.Items.IndexOf(ddlCitizenStatus.Items.FindByValue(citizen.CitizenStatus.ID.ToString()));
        }

        /// <summary>
        /// Enable or Disable controls depends on the Citizen Operation
        /// </summary>
        /// <param name="operation"></param>
        private void EnableDisableControls(CitizenOperation operation)
        {
            switch (operation)
            {
                case CitizenOperation.Add:
                    break;
                case CitizenOperation.UserToRole:
                    txtName.Enabled = false;
                    txtSpecieType.Enabled = false;
                    ddlCitizenStatus.Enabled = false;
                    ddlRole.Enabled = true;
                    break;
                case CitizenOperation.Update:
                    txtSpecieType.Enabled = false;
                    ddlRole.Enabled = false;
                    txtName.Enabled = true;
                    ddlCitizenStatus.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Bind citizen list to Grid View.
        /// </summary>
        private void FillCitizensList()
        {
            gvCitizens.DataSource = GetCitizenList();
            gvCitizens.DataBind();
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

        /// <summary>
        /// Validate form controls data.
        /// </summary>
        /// <returns></returns>
        private bool IsValidForm()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()) || !Regex.IsMatch(txtName.Text.Trim(), ConfigurationManager.AppSettings["regexAlphaNumericSpace"]))
                return false;
            if (string.IsNullOrEmpty(txtSpecieType.Text.Trim()) || !Regex.IsMatch(txtSpecieType.Text.Trim(), ConfigurationManager.AppSettings["regexAlphaNumericSpace"]))
                return false;
            if (string.IsNullOrEmpty(ddlRole.SelectedValue))
                return false;
            if (string.IsNullOrEmpty(ddlCitizenStatus.SelectedValue))
                return false;

            return true;
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