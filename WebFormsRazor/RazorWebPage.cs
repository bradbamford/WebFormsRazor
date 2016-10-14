using System.Web.WebPages;
using WebFormsRazor.Helpers;

namespace WebFormsRazor
{
    public abstract class RazorWebPage<T> : RazorWebPage
    {
        public T Model { get; set; }
    }

    public abstract class RazorWebPage : WebPageBase
    {
        protected internal HtmlHelper Html { get; internal set; }
        protected internal UrlHelper Url { get; internal set; }

        protected override void ConfigurePage(WebPageBase parentPage)
        {
            base.ConfigurePage(parentPage);
            this.Context = parentPage.Context;
        }
    }
}