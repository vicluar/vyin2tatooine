using System;

namespace Tatooine.WebUI.UserControls
{
    public partial class ucMessage : System.Web.UI.UserControl
    {
        #region Private Properties

        public string Message { get; set; }
        public string Description { get; set; }
        public MessageType Type { get; set; }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Public Methods

        public void ShowMessage(MessageType messageType, string message, string description)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("The message cannot be empty.");

            this.Type = messageType;
            this.Message = message;
            this.Description = description;
            this.ShowMessage();
        }

        #endregion

        #region Private Methods

        private void ShowMessage()
        {
            switch (this.Type)
            {
                case MessageType.Error:
                    imgMessage.ImageUrl = "~/Images/Error.gif";
                    break;
                case MessageType.Success:
                    imgMessage.ImageUrl = "~/Images/Success.gif";
                    break;
                case MessageType.Warning:
                    imgMessage.ImageUrl = "~/Images/Warning.gif";
                    break;
            }

            lblMessage.Text = this.Message;
            pnlMessage.Visible = true;
        }

        #endregion
    }

    public enum MessageType
    {
        Error,
        Success,
        Warning
    }
}