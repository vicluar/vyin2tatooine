using Ninject;
using Ninject.Infrastructure.Disposal;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace Tatooine.WebUI.Infrastructure
{
    public class CustomPageModule : DisposableObject, IHttpModule
    {
        #region Fields

        private readonly Func<IKernel> customKernel;

        #endregion

        #region Constructor

        public CustomPageModule(Func<IKernel> ckernel)
        {
            this.customKernel = ckernel;
        }

        #endregion

        #region Public Methods

        public void Init(HttpApplication context)
        {
            this.customKernel().Inject(context);
            context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
        }

        #endregion

        #region Private Methods

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            var currentPage = HttpContext.Current.Handler as Page;
            if (currentPage != null)
            {
                currentPage.InitComplete += OnPageInitComplete;
            }
        }

        private void OnPageInitComplete(object sender, EventArgs e)
        {
            var currentPage = (Page)sender;
            this.customKernel().Inject(currentPage);
            this.customKernel().Inject(currentPage.Master);
            foreach (Control c in GetControlTree(currentPage))
            {
                this.customKernel().Inject(c);
            }
        }

        private IEnumerable<Control> GetControlTree(Control root)
        {
            foreach (Control child in root.Controls)
            {
                yield return child;
                foreach (Control c in GetControlTree(child))
                {
                    yield return c;
                }
            }
        }

        #endregion
    }
}